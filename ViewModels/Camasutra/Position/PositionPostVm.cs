using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.Camasutra.Position
{
    public class PositionPostVm
    {
        [Required(ErrorMessage = "Annotation_Required_Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Annotation_Required_Description")]
        public string Description { get; set; }
    }
}
