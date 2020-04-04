using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PotatoServer.Database.Models.Camasutra
{
    [Table("Camasutra_Position")]
    public class Position
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}
