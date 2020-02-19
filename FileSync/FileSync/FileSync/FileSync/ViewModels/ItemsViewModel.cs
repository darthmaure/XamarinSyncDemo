using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class ItemsViewModel : BaseMobileViewModel
    {
        private readonly ISyncService _syncService;

        public ObservableCollection<SyncItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "All items";
            Items = new ObservableCollection<SyncItem>();
            LoadItemsCommand = new Command(async () => await OnLoadItemsAsync());

            _syncService = DependencyService.Get<ISyncService>();
        }

        async Task OnLoadItemsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _syncService.GetFilesAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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
    }
}