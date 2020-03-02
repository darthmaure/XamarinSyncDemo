using System.Threading.Tasks;
using FileSync.Shared.Models;

namespace FileSync.Services
{
    public interface IDeleteItemService
    {
        Task DeleteItemAsync(SyncItem item, bool navigateHomeOnSuccess = true);
    }
}