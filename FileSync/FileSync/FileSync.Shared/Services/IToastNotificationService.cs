namespace FileSync.Shared.Services
{
    public interface IToastNotificationService
    {
        void ShowToast(string message, bool error = false);
    }
}
