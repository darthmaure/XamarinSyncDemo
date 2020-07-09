using System;
using FileSync.Shared.Services;
using Notifications.Wpf;

namespace FileSync.Client.Services
{
    public class ToastNotificationService : IToastNotificationService
    {
        private readonly NotificationManager _notificationManager = new NotificationManager();

        public void ShowToast(string message, bool error = false)
        {
            var content = new NotificationContent
            {
                Message = message,
                Type = error ? NotificationType.Error : NotificationType.Success
            };
            _notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));
        }
    }
}
