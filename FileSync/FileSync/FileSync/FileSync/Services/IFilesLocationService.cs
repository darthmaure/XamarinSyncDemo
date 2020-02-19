using System.Collections.Generic;

namespace FileSync.Services
{
    public interface IFilesLocationService
    {
        IList<string> GetActualPathForFiles(object intent, object context);
    }
}