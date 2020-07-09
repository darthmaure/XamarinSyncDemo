using System;
using System.IO;
using FileSync.Shared.Services;

namespace FileSync.Client.Services
{
    public class Logger : ILogger
    {
        private readonly string _file = "log.log";
        public void Log(string message)
        {
            var path = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), _file);
            File.AppendAllText(path, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}{Environment.NewLine}");
        }
    }
}
