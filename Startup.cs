using PotatoServer.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using PotatoServer.Hubs;
using PotatoServer.Services.Interfaces;
using PotatoServer.Services.Implementations;
using PotatoServer.Database.MongoDb.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace PotatoServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.SetupCors();
            services.AddControllers()
                    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.SetupMvc();
            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.SetupIdentity(_configuration);
            services.SetupAuthentication(_configuration);
            services.SetupHealthChecks(_configuration);
            services.AddSignalR(x => x.EnableDetailedErrors = true);

            services.Configure<MongoDbSettings>(_configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton<IWaitingRoomService, WaitingRoomService>();

            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IUserIdProvider, EmailBasedUserIdProvider>();

            services.AddTransient<IMapperService, MapperService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<PotatoHub>("/hub/potato");
            });
        }
    }
}
