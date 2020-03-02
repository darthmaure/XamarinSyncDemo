using System.Threading.Tasks;
using FileSync.Shared.Models;

namespace FileSync.Services
{
    public interface IDownloadItemService
    {
        Task DownloadItem(SyncItem item);
    }
}