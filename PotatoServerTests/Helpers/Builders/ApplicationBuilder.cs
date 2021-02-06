using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;

namespace ListNestTests.Helpers.Builders
{
    class ApplicationBuilder<TStartup, TContext> where TStartup : class
                                                 where TContext : DbContext
    {
        private WebApplicationFactory<TStartup> _factory;
        private TContext _db;
        private IEnumerable<Action> _actions;

        public ApplicationBuilder(WebApplicationFactory<TStartup> factory)
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
                    services.AddDbContext<TContext>(options =>
                    {
                        options.UseSqlite();
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        _db = scopedServices.GetRequiredService<TContext>();

                        if (_db == null)
                            throw new TestExecutionException("DbContext is null");

                        _db.Database.EnsureDeleted();
                        _db.Database.EnsureCreated();

                        foreach(var action in _actions)
                            action.Invoke();

                        _db.SaveChanges();
                    }
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
