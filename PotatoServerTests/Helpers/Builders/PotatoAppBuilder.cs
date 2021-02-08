using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServer;

namespace PotatoServerTests.Helpers.Builders
{
    internal class PotatoAppBuilder : AppBuilder<BaseStartup, TestDbContext>
    {
        public PotatoAppBuilder(WebApplicationFactory<BaseStartup> factory) : base(factory) { }
    }
}
