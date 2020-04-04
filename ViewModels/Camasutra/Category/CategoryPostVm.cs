using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.Camasutra.Category
{
    public class CategoryPostVm
    {
        [Required(ErrorMessage = "Annotation_Required_Name")]
        public string Name { get; set; }
    }
}
