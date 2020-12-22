using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using PotatoServer.Database;
using PotatoServer.Database.Models.Core;
using PotatoServer.Exceptions;
using PotatoServer.Filters.HandleException;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PotatoServer
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection SetupCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .WithOrigins("http://192.168.0.115:4200")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            return services;
        }

        public static IServiceCollection SetupMvc(this IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add(new HandleExceptionFilterAttribute());
            })
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResources));
            });
            return services;
        }

        public static IServiceCollection SetupIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(o =>
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
            .AddEntityFrameworkStores<DatabaseContext>()
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
                            (path.StartsWithSegments("/hub")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }

        public static IServiceCollection SetupHealthChecks(this IServiceCollection services, IConfiguration configuration)
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
                        catch (SqlException)
                        {
                            return HealthCheckResult.Unhealthy();
                        }
                    }
                });
            return services;
        }
    }
}
