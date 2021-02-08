using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PotatoServer;
using System.Linq;

namespace PotatoServerTests.Configuration
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
        }
    }
}
