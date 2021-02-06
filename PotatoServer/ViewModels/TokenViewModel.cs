using System;

namespace PotatoServer.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
