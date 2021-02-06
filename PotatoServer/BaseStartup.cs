using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PotatoServer.Filters.ExceptionHandler;

namespace PotatoServer
{
    public class BaseStartup
    {
        public BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual IConfiguration Configuration { get; }

        public bool IsDevelopement => Configuration["IsDevelopement"] == "True";

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.SetupMvc();

            //services.SetupIdentity<User, CoreDatabaseContext>(Configuration);
            //services.AddDbContext<CoreDatabaseContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.SetupSqlHealthCheck(Configuration);
            services.AddTransient<DefaultExceptionHandler>();
            services.AddTransient<HideExceptionHandler>();

            if (IsDevelopement)
                services.AddTransient<IExceptionHandler, DefaultExceptionHandler>();
            else
                services.AddTransient<IExceptionHandler, HideExceptionHandler>();

        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
