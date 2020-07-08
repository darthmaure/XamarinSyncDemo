using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FileSync.Resources;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.Services
{
    public class DownloadItemService : IDownloadItemService
    {
        private readonly ISyncService _syncService;
        private readonly IToastNotificationService _toastNotificationService;

        public DownloadItemService()
        {
            _syncService = DependencyService.Get<ISyncService>();
            _toastNotificationService = DependencyService.Get<IToastNotificationService>();
        }

        public async Task DownloadItem(SyncItem item)
        {
            try
            {
                var url = await _syncService.GetDownloadFileUrlAsync(item.Name);
                await Xamarin.Essentials.Browser.OpenAsync(url);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                _toastNotificationService.ShowToast(AppResources.ToastCannotDownloadFile);
            }
        }
    }
}