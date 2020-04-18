using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.Camasutra.Category
{
    public class CategoryPutVm
    {
        [Required(ErrorMessage = "Annotation_Required_Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Annotation_Required_Name")]
        public string Name { get; set; }
    }
}
