using PotatoServer.ViewModels.Camasutra.Position;
using System.Collections.Generic;

namespace PotatoServer.ViewModels.Camasutra.Category
{
    public class CategoryGetVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PositionGetVm> Positions { get; set; }
    }
}
