using System.Windows.Threading;

namespace FileSync.Client.Services
{
    public class ToastNotificationManager : INotificationManager
    {
        public void CreateNotificationArea() => Dispatcher.CurrentDispatcher.BeginInvoke(() => new ToastNotificationWindow().Show());
    }
}
