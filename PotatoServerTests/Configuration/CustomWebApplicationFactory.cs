using ListNest.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ListNestTests.Configuration
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(context => context.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (dbContext != null)
                    services.Remove(dbContext);

                services.AddTransient<DataSeeder>();
            });
        }
    }
}
