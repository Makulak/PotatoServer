using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PotatoServer.Database.Models;
using PotatoServerTests;
using System;

namespace PotatoServerTestsCore.Configuration
{
    public class PotatoWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configure only things, that must be the same in all tests
            });
            builder.Configure(config =>
            {
                var userManager = config.ApplicationServices.GetService(typeof(UserManager<User>)) as UserManager<User>;

                if (userManager == null)
                    throw new NullReferenceException("UserManager is null"); // TODO: Message
            });
        }
    }
}
