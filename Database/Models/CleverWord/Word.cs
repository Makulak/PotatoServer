using System.ComponentModel.DataAnnotations.Schema;

namespace PotatoServer.Database.Models.CleverWord
{
    [Table("CleverWord_Word")]
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
    }
}
