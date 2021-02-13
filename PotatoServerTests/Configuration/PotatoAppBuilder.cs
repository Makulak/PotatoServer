using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServer;
using PotatoServer.Database;
using PotatoServerTestsCore.Builders;

namespace PotatoServerTestsCore.Configuration
{
    internal class PotatoAppBuilder : AppBuilder<Startup, PotatoDbContext>
    {
        public PotatoAppBuilder(WebApplicationFactory<Startup> factory) : base(factory) { }
    }
}
