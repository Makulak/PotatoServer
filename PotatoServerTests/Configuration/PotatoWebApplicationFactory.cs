using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using PotatoServer;
using PotatoServer.Database;
using PotatoServerTests.Configuration;
using PotatoServerTestsCore.Extensions;
using System.Linq;

namespace PotatoServerTestsCore.Configuration
{
    public class PotatoWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configure only things, that must be the same in all tests
                var dbContext = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<PotatoDbContext>));

                if (dbContext != null)
                    services.Remove(dbContext);
            })
            .WithAdditionalControllers(typeof(TestAuthController));
        }
    }
}
