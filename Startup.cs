using PotatoServer.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PotatoServer.Hubs.TicTacToe;
using PotatoServer.Hubs.Rooms;
using Microsoft.AspNetCore.SignalR;

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

            services.AddSingleton<IRoomRepository, RoomsRepository>();
            services.AddSingleton<IUserIdProvider, EmailBasedUserIdProvider>();
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
                endpoints.MapHub<TicTacToeHub>("/tic-tac-toe");
                endpoints.MapHub<RoomsHub>("/hub/rooms");
            });
        }
    }
}
