using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PotatoServer;
using PotatoServer.Database.Models;
using PotatoServerTestsCore;

namespace PotatoServerTests
{
    public class Startup : BaseStartup
    {
        public override IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services)
        {
            services.SetupIdentity<User, TestDbContext>(Configuration);
            services.AddDbContext<TestDbContext>(o => o.UseSqlite("Data Source=:memory:"));

            base.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}
