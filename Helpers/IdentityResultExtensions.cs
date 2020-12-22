using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace PotatoServer.Helpers
{
    public static class IdentityResultExtensions
    {
        public static string GetErrorString(this IdentityResult identityResult)
        {
            return string.Concat(identityResult.Errors.Select(e => e.Description));
        }
    }
}
