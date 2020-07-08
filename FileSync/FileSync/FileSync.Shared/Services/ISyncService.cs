using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileSync.Shared.Models;

namespace FileSync.Shared.Services
{
    public interface ISyncService
    {
        Task<bool> DeleteFileAsync(SyncItem item);
        Task<string> GetDownloadFileUrlAsync(string filename);
        Task<IList<SyncItem>> GetFilesAsync();
        Task<bool> UploadFilesAsync(IEnumerable<string> files, Action<int, int> onProgressChanged);
    }
}
