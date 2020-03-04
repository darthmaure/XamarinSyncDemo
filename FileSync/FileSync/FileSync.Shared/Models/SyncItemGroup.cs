using System.Collections.Generic;

namespace FileSync.Shared.Models
{
    public class SyncItemGroup : List<SyncItem>
    {
        public string Name { get; private set; }

        public SyncItemGroup(string name, IEnumerable<SyncItem> items) : base(items)
        {
            Name = name;
        }
    }
}
