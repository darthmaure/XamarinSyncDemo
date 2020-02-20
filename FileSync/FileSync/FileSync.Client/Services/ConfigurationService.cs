using System.IO;
using System.Threading.Tasks;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Newtonsoft.Json;

namespace FileSync.Client.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string ConfigFileName = "config.json";

        public async Task ClearAsync()
        {
            await Task.Run(() => 
            {
                if (File.Exists(ConfigFileName))
                {
                    File.Delete(ConfigFileName);
                }
            });
        }

        public async Task<Configuration> LoadAsync()
        {
            if (!File.Exists(ConfigFileName))
            {
                await SaveAsync(new Configuration { }).ConfigureAwait(true);
            }

            var content = File.ReadAllText(ConfigFileName);
            //TODO: add encryption
            return JsonConvert.DeserializeObject<Configuration>(content);
        }

        public async Task SaveAsync(Configuration configuration)
        {
            await Task.Run(() => 
            {
                var content = JsonConvert.SerializeObject(configuration);
                //TODO: encrypt
                File.WriteAllText(ConfigFileName, content);
            });
        }
    }
}
