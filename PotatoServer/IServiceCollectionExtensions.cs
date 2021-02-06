using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using PotatoServer.Exceptions;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoServer
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection SetupCors(this IServiceCollection services, params string[] addresses)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins(addresses)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            return services;
        }

        public static IServiceCollection SetupIdentity<TUser, TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TUser : IdentityUser
            where TDbContext : DbContext
        {
            services.AddIdentity<TUser, IdentityRole>(o =>
            {
                try
                {
                    o.Password.RequiredLength = int.Parse(configuration["Password:RequiredLength"]);
                    o.Password.RequireDigit = bool.Parse(configuration["Password:RequireDigit"]);
                    o.Password.RequireLowercase = bool.Parse(configuration["Password:RequireLowercase"]);
                    o.Password.RequireUppercase = bool.Parse(configuration["Password:RequireUppercase"]);
                    o.Password.RequireNonAlphanumeric = bool.Parse(configuration["Password:RequireNonAlphanumeric"]);
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
            .AddEntityFrameworkStores<TDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection SetupAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration["tokens:issuer"],
                    ValidAudience = configuration["tokens:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["tokens:key"]))
                };
                cfg.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = (context) =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }

        internal static IServiceCollection SetupMvc(this IServiceCollection services)
        {
            services.AddMvc()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResources));
            });
            return services;
        }

        internal static IServiceCollection SetupSqlHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("SQL Database Check", () =>
                {
                    using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                    {
                        try
                        {
                            conn.Open();
                            return HealthCheckResult.Healthy();
                        }
                        catch (Exception)
                        {
                            return HealthCheckResult.Unhealthy();
                        }
                    }
                });
            return services;
        }
    }
}
