using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PotatoServer.Database;
using PotatoServer.Database.Models;

namespace PotatoServer
{
    public class Startup : BaseStartup
    {
        public override IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.SetupIdentity<User, PotatoDbContext>(Configuration);
            services.AddDbContext<PotatoDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}
