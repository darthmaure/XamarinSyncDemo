using Android.Widget;
using FileSync.Shared.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSync.Droid.Services.ToastNotificationService))]
namespace FileSync.Droid.Services
{
    public class ToastNotificationService : IToastNotificationService
    {
        public void ShowToast(string message, bool error = false)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show());
        }
    }
}