using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.CleverWord.Word
{
    public class WordPostVm
    {
        [Required(ErrorMessage = "Annotation_Required_Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Annotation_Required_Definition")]

        public string Definition { get; set; }
    }
}
