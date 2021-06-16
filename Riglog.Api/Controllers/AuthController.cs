using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Login
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Failed</response>
        /// <returns>Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                return Ok(await _authService.Login(username, password));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}