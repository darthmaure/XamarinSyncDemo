namespace FileSync.Shared.Services
{
    public interface INotificationService
    {
        void Notify(string title, string message, string parameters = null);
    }
}
