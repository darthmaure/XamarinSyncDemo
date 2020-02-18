using System.Threading.Tasks;
using FileSync.Shared.Models;

namespace FileSync.Shared.Services
{
    public interface IConfigurationService
    {
        Task<Configuration> LoadAsync();
        Task ClearAsync();
        Task SaveAsync(Configuration configuration);
    }
}
