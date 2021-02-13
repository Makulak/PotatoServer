using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PotatoServer;
using PotatoServer.Database;
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

                // This method adds AuthController from tests
                //services
                //.AddControllers()
                //.AddApplicationPart(typeof(Startup).Assembly);
            });
            builder.Configure(config =>
            {
            });
        }
    }
}
