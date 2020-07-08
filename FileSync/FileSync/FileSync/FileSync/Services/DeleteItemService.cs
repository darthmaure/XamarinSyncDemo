using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FileSync.Resources;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.Services
{
    public class DeleteItemService : IDeleteItemService
    {
        private readonly ISyncService _syncService;
        private readonly INavigationService _navigationService;
        private readonly IToastNotificationService _toastNotificationService;

        public DeleteItemService()
        {
            _syncService = DependencyService.Get<ISyncService>();
            _navigationService = DependencyService.Get<INavigationService>();
            _toastNotificationService = DependencyService.Get<IToastNotificationService>();
        }


        public async Task DeleteItemAsync(SyncItem item, bool navigateHomeOnSuccess = true)
        {
            var confirm = await Shell.Current.DisplayAlert(
                AppResources.AlertTitleFileDelete, 
                string.Format(AppResources.AlertTextFileDelete, item.Name), 
                AppResources.LabelOK, AppResources.LabelCancel);
            if (!confirm) return;

            try
            {
                var result = await _syncService.DeleteFileAsync(item);
                _toastNotificationService.ShowToast(result ? string.Format(AppResources.ToastFileDeleted, item.Name) : AppResources.ToastFileDeletionError);

                if (result && navigateHomeOnSuccess)
                {
                    await _navigationService.NavigateHomeAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}