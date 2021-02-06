using System.Collections.Generic;

namespace PotatoServer.ViewModels.Core
{
    public class PagedViewModel<T>
    {
        public PagedViewModel(List<T> items, int totalItemsCount)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
        }

        public List<T> Items { get; set; }
        public int TotalItemsCount { get; set; }
    }
}
