﻿using System.ComponentModel.DataAnnotations;

namespace PotatoServer.ViewModels.User
{
    public class UserLoginVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
