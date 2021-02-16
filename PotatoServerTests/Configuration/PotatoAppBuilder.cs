using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServer;
using PotatoServer.Database;
using PotatoServer.Database.Models;
using PotatoServerTestsCore.Builders;

namespace PotatoServerTestsCore.Configuration
{
    internal class PotatoAppBuilder : AppBuilder<Startup, PotatoDbContext, PotatoUser>
    {
        public PotatoAppBuilder(WebApplicationFactory<Startup> factory) : base(factory) { }
    }
}
