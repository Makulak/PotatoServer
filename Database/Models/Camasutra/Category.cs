using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PotatoServer.Database.Models.Camasutra
{
    [Table("Camasutra_Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}
