using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Essentials;

namespace FileSync.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public async Task ClearAsync()
        {
            SecureStorage.Remove(nameof(Configuration.ApiKey));
            SecureStorage.Remove(nameof(Configuration.Bucket));
            SecureStorage.Remove(nameof(Configuration.DatabaseUrl));
            SecureStorage.Remove(nameof(Configuration.Email));
            SecureStorage.Remove(nameof(Configuration.Password));
            await Task.CompletedTask;
        }

        public async Task<Configuration> LoadAsync()
        {
            return new Configuration
            {
                ApiKey = await SecureStorage.GetAsync(nameof(Configuration.ApiKey)),
                Bucket = await SecureStorage.GetAsync(nameof(Configuration.Bucket)),
                DatabaseUrl = await SecureStorage.GetAsync(nameof(Configuration.DatabaseUrl)),
                Email = await SecureStorage.GetAsync(nameof(Configuration.Email)),
                Password = await SecureStorage.GetAsync(nameof(Configuration.Password))
            };
        }

        public async Task SaveAsync(Configuration configuration)
        {
            await SecureStorage.SetAsync(nameof(Configuration.ApiKey), configuration.ApiKey);
            await SecureStorage.SetAsync(nameof(Configuration.Bucket), configuration.Bucket);
            await SecureStorage.SetAsync(nameof(Configuration.DatabaseUrl), configuration.DatabaseUrl);
            await SecureStorage.SetAsync(nameof(Configuration.Email), configuration.Email);
            await SecureStorage.SetAsync(nameof(Configuration.Password), configuration.Password);
        }
    }
}