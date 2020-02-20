using System.Threading.Tasks;

namespace FileSync.Client.Services
{
    public interface IDownloadService
    {
        Task DownloadFile(string url, string filename, bool openFile);
    }
}
