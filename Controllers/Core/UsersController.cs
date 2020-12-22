﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PotatoServer.Exceptions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using PotatoServer.Database.Models.Core;
using PotatoServer.ViewModels.Core.User;
using PotatoServer.Filters.LoggedAction;
using PotatoServer.Helpers;

namespace PotatoServer.Controllers.Core
{
    [Route("api/auth")]
    [ApiController]
    public class UsersController : Controller
    {
        private UserManager<User> _userManager;
        private IStringLocalizer<SharedResources> _localizer;
        private IConfiguration _configuration;

        public UsersController(UserManager<User> userManager,
            IStringLocalizer<SharedResources> localizer,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _localizer = localizer;
            _configuration = configuration;
        }

        [HttpPost("signin")]
        [LoggedActionFilter]
        public async Task<ActionResult<UserLoginResponseVm>> Login([FromBody]UserLoginVm userVm)
        {
            var user = await _userManager.FindByEmailAsync(userVm.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userVm.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    new Claim("UserId", user.Id)
                };

                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                var token = new JwtSecurityToken(
                    _configuration["Tokens:Issuer"],
                    _configuration["Tokens:Audience"],
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Tokens:Expires"])),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));

                return Ok(new UserLoginResponseVm()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expires = token.ValidTo
                });
            }
            throw new BadRequestException(_localizer.GetString("WrongEmailOrPassword"));
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] UserRegisterVm userVm)
        {
            var user = new User
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