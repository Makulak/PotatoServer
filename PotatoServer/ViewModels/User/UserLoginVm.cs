using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.Core.User
{
    public class UserLoginVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}
