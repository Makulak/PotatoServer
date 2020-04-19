using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PotatoServer.ViewModels.ServerSettings.Core;
using System.Threading.Tasks;

namespace PotatoServer.Controllers.Core
{
    [Route("api/core")]
    [ApiController]
    public class CoreController : Controller
    {
        private IConfiguration _configuration;

        public CoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("server-settings")]
        public async Task<ActionResult<ServerSettingsGetVm>> GetServerSettings()
        {
            return await Task.FromResult(new ServerSettingsGetVm
            {
                RequiredLength = int.Parse(_configuration["Password:RequiredLength"]),
                RequireDigit = bool.Parse(_configuration["Password:RequireDigit"]),
                RequireLowercase = bool.Parse(_configuration["Password:RequireLowercase"]),
                RequireUppercase = bool.Parse(_configuration["Password:RequireUppercase"]),
                RequireNonAlphanumeric = bool.Parse(_configuration["Password:RequireNonAlphanumeric"])
            });
        }
    }
}
