using PotatoServerTests.Extensions;
using PotatoServerTestsCore.Configuration;
using PotatoServerTestsCore.Helpers.Builders;
using Xunit;

namespace PotatoServerTestsCore.Tests
{
    public class SignInTests : IClassFixture<PotatoWebApplicationFactory>
    {
        private readonly PotatoWebApplicationFactory _factory;

        public SignInTests(PotatoWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void SignIn_Should_ReturnUnauthorized_When_UserIsNotLoggedIn()
        {
            var address = "api/auth";
            var client = new PotatoAppBuilder(_factory)
                        .CreateClient();

            var response = await client.GetUserTokenAsync("admin@admin.com", "Admin");
        }
    }
}
