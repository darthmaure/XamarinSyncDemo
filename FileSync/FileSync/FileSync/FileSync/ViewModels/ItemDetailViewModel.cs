using System;

using FileSync.Shared.Models;

namespace FileSync.ViewModels
{
    public class ItemDetailViewModel : BaseMobileViewModel
    {
        public SyncItem Item { get; set; }
        public ItemDetailViewModel(SyncItem item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
