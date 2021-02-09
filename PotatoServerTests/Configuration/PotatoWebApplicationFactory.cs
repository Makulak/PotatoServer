using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PotatoServer;
using PotatoServer.Database;
using PotatoServer.Database.Models;
using System;
using System.Linq;

namespace PotatoServerTestsCore.Configuration
{
    public class PotatoWebApplicationFactory : WebApplicationFactory<BaseStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configure only things, that must be the same in all tests

                var dbContext = services.SingleOrDefault(context => context.ServiceType == typeof(DbContextOptions<TestDbContext>));
                if (dbContext != null)
                    services.Remove(dbContext);

                services.AddTransient<DataSeeder>();
            });
            builder.Configure(config =>
            {
                var userManager = config.ApplicationServices.GetService(typeof(UserManager<User>)) as UserManager<User>;

                if (userManager == null)
                    throw new NullReferenceException("UserManager is null"); // TODO: Message

                DatabaseSeeder.AddAdmin(userManager);
            });
        }
    }
}
