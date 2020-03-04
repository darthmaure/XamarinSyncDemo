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
        private readonly IDateToHeaderFormatService _dateToHeaderFormatService;

        public ObservableCollection<SyncItemGroup> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command TapItemCommand { get; set; }
        public Command SwipeItemCommand { get; set; }

        private long totalSize;

        public long TotalSize
        {
            get => totalSize;
            set => SetProperty(ref totalSize, value);
        }

        private long itemsCount;

        public long ItemsCount
        {
            get => itemsCount;
            set => SetProperty(ref itemsCount, value);
        }


        public ItemsViewModel()
        {
            _syncService = DependencyService.Get<ISyncService>();
            _deleteItemService = DependencyService.Get<IDeleteItemService>();
            _downloadItemService = DependencyService.Get<IDownloadItemService>();
            _dateToHeaderFormatService = DependencyService.Get<IDateToHeaderFormatService>();

            Items = new ObservableCollection<SyncItemGroup>();
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
                foreach (var item in items
                    .GroupBy(d => _dateToHeaderFormatService.Format(d.CreateDate))
                    .OrderByDescending(d => d.Max(y => y.CreateDate)))
                {
                    Items.Add(new SyncItemGroup(item.Key, item.OrderByDescending(d => d.CreateDate)));
                }
                TotalSize = items.Sum(d => d.Length);
                ItemsCount = items.Count;
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