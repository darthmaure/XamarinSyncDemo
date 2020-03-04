using FileSync.Shared.Models;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class ItemDetailViewModel : BaseItemViewModel
    {
        public SyncItem Item { get; set; }

        public ItemDetailViewModel(SyncItem item = null) : base()
        {
            Item = item;

            DownloadItemCommand = new Command(async () => await OnDownloadItem(Item));
            DeleteItemCommand = new Command(async () => await OnDeleteItem(Item));
        }
    }
}
