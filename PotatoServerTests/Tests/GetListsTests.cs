using ListNest;
using ListNestTests.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ListNestTests
{
    public class GetListsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetListsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public void GetLists_Should_ReturnUnauthorized_When_UserIsNotLoggedIn()
        {
            var address = "api/lists";
        }
    }
}
