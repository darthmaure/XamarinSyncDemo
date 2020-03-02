using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FileSync.Services;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class ItemsViewModel : BaseMobileViewModel
    {
        private readonly ISyncService _syncService;
        private readonly IDeleteItemService _deleteItemService;
        private readonly IDownloadItemService _downloadItemService;

        public ObservableCollection<SyncItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command TapItemCommand { get; set; }
        public Command SwipeItemCommand { get; set; }

        private long totalSize;

        public long TotalSize
        {
            get => totalSize;
            set => SetProperty(ref totalSize, value);
        }


        public ItemsViewModel()
        {
            _syncService = DependencyService.Get<ISyncService>();
            _deleteItemService = DependencyService.Get<IDeleteItemService>();
            _downloadItemService = DependencyService.Get<IDownloadItemService>();

            Items = new ObservableCollection<SyncItem>();
            LoadItemsCommand = new Command(async () => await OnLoadItemsAsync());
        }

        private async Task OnLoadItemsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                TotalSize = 0;
                Items.Clear();
                var items = await _syncService.GetFilesAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                TotalSize = items.Sum(d => d.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async Task OnAppearing() => await OnLoadItemsAsync();
    }
}