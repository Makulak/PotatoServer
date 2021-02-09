using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServer;

namespace PotatoServerTestsCore.Helpers.Builders
{
    internal class PotatoAppBuilder : AppBuilder<BaseStartup, TestDbContext>
    {
        public PotatoAppBuilder(WebApplicationFactory<BaseStartup> factory) : base(factory) { }
    }
}
