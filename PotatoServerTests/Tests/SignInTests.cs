using PotatoServer.ViewModels;
using PotatoServerTests.Extensions;
using PotatoServerTestsCore.Configuration;
using PotatoServerTestsCore.Extensions;
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
        public async void SignIn_Should_SignIn()
        {
            var address = "api/core/server-settings";
            var client = new PotatoAppBuilder(_factory)
                        .CreateClient();

            var response = await client.GetUserTokenAsync("admin@admin.pl","Admin");

            var responseTwo = await client.DoGetAsync<ServerSettingsVmResult>(address);

            Assert.Equal(System.Net.HttpStatusCode.OK, responseTwo.StatusCode);
        }
    }
}
