using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServer;

namespace PotatoServerTestsCore.Helpers.Builders
{
    internal class PotatoAppBuilder : AppBuilder<Startup, TestDbContext>
    {
        public PotatoAppBuilder(WebApplicationFactory<Startup> factory) : base(factory) { }
    }
}
