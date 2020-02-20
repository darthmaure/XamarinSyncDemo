using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FileSync.Client.Services
{
    public class DownloadService : IDownloadService
    {
        public async Task DownloadFile(string url, string filename, bool openFile)
        {
            await Task.Run(() =>
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                string destinationpath = Path.GetTempPath();
                var file = Path.Combine(destinationpath, filename);
                if (!Directory.Exists(destinationpath))
                {
                    Directory.CreateDirectory(destinationpath);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result)
                {
                    var responseStream = response.GetResponseStream();
                    using var fileStream = File.Create(file);
                    responseStream.CopyTo(fileStream);
                }

                if (openFile)
                    Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
            });
        }
    }
}
