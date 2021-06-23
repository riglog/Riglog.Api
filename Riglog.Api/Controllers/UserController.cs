using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Riglog.Api.Services.Interfaces;
using Riglog.Api.Services.Models;

namespace Riglog.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// List all users
        /// </summary>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserModel>))]
        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            return Ok(await _userService.GetAllAsync());
        }
        
        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="userId">Guid</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            try
            {
                return Ok(await _userService.GetByIdAsync(userId));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        /// <summary>
        /// Create new user
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Missing required fields</response>
        /// <response code="409">User already exists</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            try
            {
                return Ok(await _userService.CreateAsync(userModel));
            }
            catch (Exception)
            {
                return Conflict();
            }
        }
        
        /// <summary>
        /// Update user
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Missing required fields</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserModel userModel)
        {
            try
            {
                return Ok(await _userService.UpdateAsync(userModel));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId">Guid</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response> 
        [HttpDelete, Route("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                await _userService.DeleteAsync(userId);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Set user password
        /// </summary>
        /// <param name="userId">Guid</param>
        /// <param name="password">Password</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("{userId:guid}/set-password")]
        public async Task<IActionResult> SetPassword(Guid userId, [FromBody] string password)
        {
            try
            {
                await _userService.SetPasswordAsync(userId, password);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}