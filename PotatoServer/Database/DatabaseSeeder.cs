using Microsoft.AspNetCore.Identity;
using PotatoServer.Exceptions;

namespace PotatoServer.Database
{
    public class DatabaseSeeder
    {
        public static async void AddAdmin<T>(UserManager<T> userManager) where T : IdentityUser, new()
        {
            string adminEmail = "admin@admin.com";
            string adminPassword = "Admin";
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                T user = new T
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                IdentityResult result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                else
                    throw new ServerErrorException("Cannot create default admin"); // TODO: Message
            }
        }
    }
}
