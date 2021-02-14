using Microsoft.AspNetCore.Identity;
using PotatoServer.Exceptions;
using PotatoServer.Helpers.Extensions;
using System.Threading.Tasks;

namespace PotatoServer.Database
{
    public class DatabaseSeeder
    {
        public static async Task AddAdminAsync<T>(UserManager<T> userManager, string email, string userName, string password) where T : IdentityUser, new()
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                T user = new T
                {
                    UserName = userName,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                else
                    throw new ServerErrorException($"Cannot create default admin: \r\n {result.GetErrorString()}");
            }
        }
    }
}
