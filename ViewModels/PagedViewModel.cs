using System.Collections.Generic;

namespace PotatoServer.ViewModels.Core
{
    public class PagedViewModel<T>
    {
        public List<T> Items { get; set; }
        public int TotalItemsCount { get; set; }
    }
}
