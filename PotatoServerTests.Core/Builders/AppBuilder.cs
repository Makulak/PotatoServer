using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Identity;

namespace PotatoServerTestsCore.Builders
{
    public abstract class AppBuilder<TStartup, TContext, TUser> : IDisposable
                                                         where TStartup : class
                                                         where TContext : DbContext
                                                         where TUser : IdentityUser
    {
        private readonly WebApplicationFactory<TStartup> _factory;
        
        private List<Action> _actions;
        private DbConnection _dbConnection;

        protected UserManager<TUser> _userManager;
        protected TContext _dbContext;

        public AppBuilder(WebApplicationFactory<TStartup> factory)
        {
            _factory = factory;
            _actions = new List<Action>();
        }

        protected void AddAction(Action action)
        {
            _actions.Add(action);
        }

        protected void CreateUsers(TUser[] users, string password)
        {
            _actions.Add(async () =>
            {
                foreach (var user in users)
                    await _userManager.CreateAsync(user, password);
            });
        }

        public HttpClient CreateClient()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Configure only things, that may vary in different tests
                    // Or should be done for each test separetly
                    _dbConnection = new SqliteConnection("Filename=:memory:");
                    _dbConnection.Open();

                    services.AddDbContext<TContext>(options =>
                    {
                        options.UseSqlite(_dbConnection);
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        _dbContext = scopedServices.GetRequiredService<TContext>();
                        _userManager = scopedServices.GetRequiredService<UserManager<TUser>>();

                        if (_dbContext == null)
                            throw new NullReferenceException("DbContext is null");

                        _dbContext.Database.EnsureDeleted();
                        _dbContext.Database.EnsureCreated();

                        foreach(var action in _actions)
                            action.Invoke();

                        _dbContext.SaveChanges();
                    }
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public void Dispose()
        {
            _dbConnection.Close();
        }
    }
}
