using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class ItemDetailViewModel : BaseMobileViewModel
    {
        private readonly ISyncService _syncService;
        private readonly INavigationService _navigationService;
        private readonly IToastNotificationService _toastNotificationService;

        public SyncItem Item { get; set; }
        public ICommand DownloadCommand { get; }
        public ICommand DeleteCommand { get; }

        public ItemDetailViewModel(SyncItem item = null)
        {
            Item = item;

            _syncService = DependencyService.Get<ISyncService>();
            _navigationService = DependencyService.Get<INavigationService>();
            _toastNotificationService = DependencyService.Get<IToastNotificationService>();

            DownloadCommand = new Command(async () => await DownloadFile());
            DeleteCommand = new Command(async () => await DeleteFile());
        }

        private async Task DownloadFile() 
        {
            IsBusy = true;
            try
            {
                var url = await _syncService.GetDownloadFileUrlAsync(Item.Name);
                await Xamarin.Essentials.Browser.OpenAsync(url);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                _toastNotificationService.ShowToast("Cannot download file");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task DeleteFile() 
        {
            var confirm = await Shell.Current.DisplayAlert("Delete file", $"Do you really want to delete file {Item.Name}?", "OK", "Cancel");
            if (!confirm) return;

            IsBusy = true;
            try
            {
                var result = await _syncService.DeleteFileAsync(Item);
                _toastNotificationService.ShowToast(result ? $"File deleted: {Item.Name}" : "Cannot delete file");

                if (result)
                {
                    await _navigationService.NavigateHomeAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
