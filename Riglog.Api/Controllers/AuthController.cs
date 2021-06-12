using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Riglog.Api.Data.Entities;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        
        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Login
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Failed</response>
        /// <returns>Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var hasher = new PasswordHasher<User>();
                var user = _userRepository.GetByUsername(username);
                
                if (hasher.VerifyHashedPassword(user, user.Password, password) == 0) return BadRequest();
                
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AuthSettings:SecretKey")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var tokenOptions = new JwtSecurityToken(
                    _configuration.GetValue<string>("AuthSettings:Audience"),
                    _configuration.GetValue<string>("AuthSettings:Audience"),
                    claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: signinCredentials
                );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                
                return Ok(token);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}