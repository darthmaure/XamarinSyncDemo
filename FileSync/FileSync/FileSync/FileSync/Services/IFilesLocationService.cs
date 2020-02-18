namespace FileSync.Services
{
    public interface IFilesLocationService
    {
        string GetActualPathForFile(object uri, object context);
    }
}