using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PotatoServer;
using PotatoServer.Controllers;
using PotatoServer.Database.Models;

namespace PotatoServerTests.Configuration
{
    /// <summary>
    /// This is only because default BaseAuthController<TUser> is not created by default
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class TestAuthController : BaseAuthController<User>
    {
        public TestAuthController(UserManager<User> userManager, IStringLocalizer<SharedResources> localizer, IConfiguration configuration) : base(userManager, localizer, configuration)
        {
        }
    }
}
