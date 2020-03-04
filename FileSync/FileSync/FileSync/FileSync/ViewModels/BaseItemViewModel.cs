using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Services;
using FileSync.Shared.Models;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class BaseItemViewModel : BaseMobileViewModel
    {
        private readonly IDeleteItemService _deleteItemService;
        private readonly IDownloadItemService _downloadItemService;

        public BaseItemViewModel()
        {
            _deleteItemService = DependencyService.Get<IDeleteItemService>();
            _downloadItemService = DependencyService.Get<IDownloadItemService>();
        }

        public ICommand DownloadItemCommand { get; protected set; }
        public ICommand DeleteItemCommand { get; protected set; }

        protected async Task OnDownloadItem(SyncItem item)
        {
            IsBusy = true;
            await _downloadItemService.DownloadItem(item);
            IsBusy = false;
        }
        protected async Task OnDeleteItem(SyncItem item)
        {
            IsBusy = true;
            await _deleteItemService.DeleteItemAsync(item);
            IsBusy = false;
        }
    }
}
