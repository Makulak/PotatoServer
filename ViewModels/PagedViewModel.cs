using System.Collections.Generic;

namespace PotatoServer.ViewModels
{
    public class PagedViewModel<T>
    {
        public List<T> items { get; set; }
        public int totalItemsCount { get; set; }
    }
}
