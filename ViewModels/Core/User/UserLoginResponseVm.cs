using System;

namespace PotatoServer.ViewModels.Core.User
{
    public class UserLoginResponseVm
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
