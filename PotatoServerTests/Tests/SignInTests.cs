using PotatoServerTests.Configuration;
using PotatoServerTests.Helpers.Builders;
using Xunit;

namespace PotatoServerTests.Tests
{
    public class SignInTests : IClassFixture<PotatoWebApplicationFactory>
    {
        private readonly PotatoWebApplicationFactory _factory;

        public SignInTests(PotatoWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public void GetLists_Should_ReturnUnauthorized_When_UserIsNotLoggedIn()
        {
            var address = "api/auth";
            var client = new PotatoAppBuilder(_factory)
                        .CreateClient();
        }
    }
}
