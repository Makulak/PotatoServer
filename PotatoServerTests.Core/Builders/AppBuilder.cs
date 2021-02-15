using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace PotatoServerTestsCore.Builders
{
    public abstract class AppBuilder<TStartup, TContext> : IDisposable
                                                         where TStartup : class
                                                         where TContext : DbContext
    {
        private readonly WebApplicationFactory<TStartup> _factory;
        
        private DbConnection _dbConnection;
        
        protected  TContext _dbContext;
        protected List<Action> _actions;

        public AppBuilder(WebApplicationFactory<TStartup> factory)
        {
            _factory = factory;
            _actions = new List<Action>();
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
