using System;

namespace PotatoServer.ViewModels
{
    public class TokenVmResult
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
