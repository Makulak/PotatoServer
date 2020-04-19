using System;

namespace PotatoServer.Database.Models.Core
{
    public interface IBaseModel
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
    }
}
