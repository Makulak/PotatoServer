using PotatoServer.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PotatoServer.Services.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using PotatoServer.Exceptions;
using PotatoServer.Filters;
using PotatoServer.Database.Models.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PotatoServer.Hubs.TicTacToe;
using PotatoServer.Hubs.Rooms;

namespace PotatoServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc(config =>
            {
                config.Filters.Add(new HandleExceptionAttribute());
            })
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResources));
            });

            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>(o =>
            {
                try
                {
                    o.Password.RequiredLength = int.Parse(Configuration["Password:RequiredLength"]);
                    o.Password.RequireDigit = bool.Parse(Configuration["Password:RequireDigit"]);
                    o.Password.RequireLowercase = bool.Parse(Configuration["Password:RequireLowercase"]);
                    o.Password.RequireUppercase = bool.Parse(Configuration["Password:RequireUppercase"]);
                    o.Password.RequireNonAlphanumeric = bool.Parse(Configuration["Password:RequireNonAlphanumeric"]);
                }
                catch (ArgumentNullException ex)
                {
                    throw new ServerErrorException($"Error during setting password configuration, argument: {ex.ParamName}");
                }
                catch (FormatException ex)
                {
                    throw new ServerErrorException($"Error during setting password configuration, argument: {ex.Message}");
                }

                o.User.RequireUniqueEmail = true;
                o.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
             {
                 cfg.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidIssuer = Configuration["tokens:issuer"],
                     ValidAudience = Configuration["tokens:audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["tokens:key"]))
                 };
             });

            services.AddHealthChecks()
                .AddCheck("SQL Database Check", () =>
                {
                    using (var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                    {
                        try
                        {
                            conn.Open();
                            return HealthCheckResult.Healthy();
                        }
                        catch (SqlException)
                        {
                            return HealthCheckResult.Unhealthy();
                        }
                    }
                });
            services.AddSignalR(x => x.EnableDetailedErrors = true);

            services.AddTransient<CategoryMapper>();
            services.AddTransient<PositionMapper>();
            services.AddTransient<WordMapper>();
            services.AddSingleton<IRoomRepository, RoomsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<TicTacToeHub>("/tic-tac-toe");
                endpoints.MapHub<RoomsHub>("/api/rooms");
            });
        }
    }
}
