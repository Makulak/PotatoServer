using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PotatoServer.Exceptions;
using System.Security.Claims;
using PotatoServer.ViewModels.Core.User;
using PotatoServer.Helpers.Extensions;

namespace PotatoServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class BaseAuthController<TUser> : Controller where TUser : IdentityUser, new()
    {
        private UserManager<TUser> _userManager;
        private IStringLocalizer<SharedResources> _localizer;
        private IConfiguration _configuration;

        public BaseAuthController(UserManager<TUser> userManager,
            IStringLocalizer<SharedResources> localizer,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _localizer = localizer;
            _configuration = configuration;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login([FromBody] UserLoginVm userVm)
        {
            var user = await _userManager.FindByEmailAsync(userVm.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userVm.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Email),
                    new Claim("UserId", user.Id)
                };

                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                var token = new JwtSecurityToken(
                    _configuration["Tokens:Issuer"],
                    _configuration["Tokens:Audience"],
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Tokens:Expires"])),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expires = token.ValidTo
                });
            }
            throw new BadRequestException(_localizer.GetString("WrongEmailOrPassword"));
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register([FromBody] UserRegisterVm userVm)
        {
            var user = new TUser
            {
                UserName = userVm.Username,
                Email = userVm.Email
            };

            var result = await _userManager.CreateAsync(user, userVm.Password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));

            if (result.Succeeded)
                return Ok();

            throw new BadRequestException(result.GetErrorString());
        }
    }
}
