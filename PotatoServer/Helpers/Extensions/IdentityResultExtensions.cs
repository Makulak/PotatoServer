using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace PotatoServer.Helpers.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetErrorString(this IdentityResult identityResult)
        {
            return string.Concat(identityResult.Errors.Select(error => $"{error.Code} - {error.Description}\r\n"));
        }
    }
}
