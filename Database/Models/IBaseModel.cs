using System;

namespace PotatoServer.Database.Models
{
    interface IBaseModel
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
    }
}
