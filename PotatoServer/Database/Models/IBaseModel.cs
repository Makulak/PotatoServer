using System;

namespace PotatoServer.Database.Models
{
    public interface IBaseModel
    {
        int Id { get; set; }
        DateTime? Created { get; set; }
        DateTime? Changed { get; set; }
        bool IsDeleted { get; set; }
    }
}
