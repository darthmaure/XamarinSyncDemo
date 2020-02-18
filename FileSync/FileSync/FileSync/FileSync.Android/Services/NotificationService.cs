using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using FileSync.Shared.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSync.Droid.Services.NotificationService))]
namespace FileSync.Droid.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly int _notificationId = 1000;
        private static readonly string _channelId = "localNotificationChannel";
        private readonly Context _context;

        public NotificationService()
        {
            _context = Android.App.Application.Context;
        }

        public void Notify(string title, string message, string parameters = null)
        {
            CreateNotificationChannel();
            SendNotification(title, message, parameters);
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = "MvxCustomBinding.Android";
            var description = "channel description";
            var channel = new NotificationChannel(_channelId, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)_context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private void SendNotification(string title, string message, string parameters = null)
        {
            var icon = string.IsNullOrEmpty(parameters)
                ? Resource.Drawable.outline_cloud_upload_black_18dp
                : int.Parse(parameters);

            var builder = new NotificationCompat.Builder(_context, _channelId)
              .SetAutoCancel(true) 
              .SetContentTitle(title) 
              .SetSmallIcon(icon) 
              .SetContentText(message)
              .SetChannelId(_channelId);

            var notificationManager = NotificationManagerCompat.From(_context);
            notificationManager.Notify(_notificationId, builder.Build());
        }
    }
}