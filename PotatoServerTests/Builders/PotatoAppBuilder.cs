using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServer;
using PotatoServer.Database;

namespace PotatoServerTestsCore.Helpers.Builders
{
    internal class PotatoAppBuilder : AppBuilder<Startup, PotatoDbContext>
    {
        public PotatoAppBuilder(WebApplicationFactory<Startup> factory) : base(factory) { }
    }
}
