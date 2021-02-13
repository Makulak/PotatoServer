using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PotatoServer.Exceptions;
using PotatoServer.Filters.HandleException;
using PotatoServer.Filters.LoggedAction;
using PotatoServer.ViewModels;

namespace PotatoServer.Controllers
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
        public async Task<ActionResult> GetServerSettings()
        {
            return await Task.FromResult(Ok(new ServerSettingsVmResult
            {
                RequiredLength = int.Parse(_configuration["Password:RequiredLength"]),
                RequireDigit = bool.Parse(_configuration["Password:RequireDigit"]),
                RequireLowercase = bool.Parse(_configuration["Password:RequireLowercase"]),
                RequireUppercase = bool.Parse(_configuration["Password:RequireUppercase"]),
                RequireNonAlphanumeric = bool.Parse(_configuration["Password:RequireNonAlphanumeric"])
            }));
        }

        [HttpGet("test-status-code/{code}")]
        [HandleExceptionFilter]
        [LoggedActionFilter]
        public ActionResult GetProperStatusCode(int code)
        {
            switch (code)
            {
                case 200:
                    return Ok(new { Message = "OK" });
                case 400:
                    throw new BadRequestException("Bad request");
                case 404:
                    throw new NotFoundException("NotFound");
                case 500:
                    throw new ServerErrorException("ServerError");
                default:
                    throw new Exception("Not supported exception");
            }
        }
    }
}
