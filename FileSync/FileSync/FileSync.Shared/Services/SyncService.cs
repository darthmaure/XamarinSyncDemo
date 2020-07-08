using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileSync.Shared.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;

namespace FileSync.Shared.Services
{
    public class SyncService : ISyncService
    {
        private readonly IConfigurationService _configurationService;
        private readonly ILogger _logger;
        private const string _root = "RootDirectory";

        public SyncService(IConfigurationService configurationService, ILogger logger)
        {
            _configurationService = configurationService;
            _logger = logger;
        }
        public SyncService() => _configurationService = SimpleServiceLocator.Default.Get<IConfigurationService>();

        public async Task<bool> DeleteFileAsync(SyncItem item)
        {
            try
            {
                var configuration = await _configurationService.LoadAsync();
                var authLink = await GetAuthLink(configuration.ApiKey, configuration.Email, configuration.Password);

                var database = GetDatabaseClient(configuration.DatabaseUrl, authLink.FirebaseToken);
                var storage = GetStorage(configuration.Bucket, authLink.FirebaseToken);

                await storage.Child(_root).Child(item.Name).DeleteAsync();
                await database.Child(_root).Child(item.Id).DeleteAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                _logger?.Log($"{nameof(SyncService)} - {e}");
            }
            return false;
        }

        public async Task<string> GetDownloadFileUrlAsync(string filename)
        {
            try
            {
                var configuration = await _configurationService.LoadAsync();
                var authLink = await GetAuthLink(configuration.ApiKey, configuration.Email, configuration.Password);

                var storage = GetStorage(configuration.Bucket, authLink.FirebaseToken);

                return await storage.Child(_root).Child(filename).GetDownloadUrlAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                _logger?.Log($"{nameof(SyncService)} - {e}");
            }
            return null;
        }

        public async Task<IList<SyncItem>> GetFilesAsync()
        {
            try
            {
                var configuration = await _configurationService.LoadAsync();
                var authLink = await GetAuthLink(configuration.ApiKey, configuration.Email, configuration.Password);

                var database = GetDatabaseClient(configuration.DatabaseUrl, authLink.FirebaseToken);

                return await RetrieveItems(database);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                _logger?.Log($"{nameof(SyncService)} - {e}");
            }
            return new List<SyncItem>();
        }

        public async Task<bool> UploadFilesAsync(IEnumerable<string> files, Action<int, int> onProgressChanged)
        {
            try
            {
                var configuration = await _configurationService.LoadAsync();
                var authLink = await GetAuthLink(configuration.ApiKey, configuration.Email, configuration.Password);

                var database = GetDatabaseClient(configuration.DatabaseUrl, authLink.FirebaseToken);
                var storage = GetStorage(configuration.Bucket, authLink.FirebaseToken);
                var counter = 0;

                foreach (var file in files)
                {
                    onProgressChanged?.Invoke(++counter, default);

                    var name = Path.GetFileName(file);
                    byte[] bytes = null;
                    using(var fileStream = new FileStream(file, FileMode.Open))
                    {
                        bytes = new byte[fileStream.Length];
                        fileStream.Read(bytes, 0, (int)fileStream.Length);
                    }
                    using (var stream = new MemoryStream(bytes))
                    {
                        var uploadTask = storage.Child(_root).Child(name).PutAsync(stream);
                        uploadTask.Progress.ProgressChanged += (s, e) => onProgressChanged?.Invoke(counter, e.Percentage);

                        await uploadTask;
                        await database.Child(_root).PostAsync(new SyncItem
                        {
                            Name = name,
                            Length = bytes.Length,
                            CreateDate = DateTime.Now.ToUniversalTime()
                        });
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                _logger?.Log($"{nameof(SyncService)} - {e}");
            }
            return false;
        }

        private async Task<IList<SyncItem>> RetrieveItems(FirebaseClient database)
        {
            var items = await database.Child(_root).OnceAsync<SyncItem>();
            foreach (var item in items)
            {
                item.Object.Id = item.Key;
            }
            return items.Select(d => d.Object).ToList();
        }
        private async Task<FirebaseAuthLink> GetAuthLink(string apiKey, string email, string password)
        {
            var provider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            return await provider.SignInWithEmailAndPasswordAsync(email, password);
        }
        private FirebaseClient GetDatabaseClient(string databaseUrl, string firebaseToken) => new FirebaseClient($"https://{databaseUrl}.firebaseio.com/", new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(firebaseToken)
        });
        private FirebaseStorage GetStorage(string bucket, string firebaseToken) => new FirebaseStorage($"{bucket}.appspot.com", new FirebaseStorageOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(firebaseToken),
            ThrowOnCancel = true
        });
    }
}
