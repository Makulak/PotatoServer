using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.Core.User
{
    public class UserRegisterVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
