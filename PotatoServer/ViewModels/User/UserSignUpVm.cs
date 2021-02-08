using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.Core.User
{
    public class UserSignUpVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}
