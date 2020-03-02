using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Services;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class ItemDetailViewModel : BaseMobileViewModel
    {
        private readonly IDeleteItemService _deleteItemService;
        private readonly IDownloadItemService _downloadItemService;

        public SyncItem Item { get; set; }
        public ICommand DownloadCommand { get; }
        public ICommand DeleteCommand { get; }

        public ItemDetailViewModel(SyncItem item = null)
        {
            Item = item;

            _deleteItemService = DependencyService.Get<IDeleteItemService>();
            _downloadItemService = DependencyService.Get<IDownloadItemService>();

            DownloadCommand = new Command(async () => await DownloadFile());
            DeleteCommand = new Command(async () => await DeleteFile());
        }

        private async Task DownloadFile() 
        {
            IsBusy = true;
            await _downloadItemService.DownloadItem(Item);
            IsBusy = false;
        }
        private async Task DeleteFile() 
        {
            IsBusy = true;
            await _deleteItemService.DeleteItemAsync(Item);
            IsBusy = false;
        }
    }
}
