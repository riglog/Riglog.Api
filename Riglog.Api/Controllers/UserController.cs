using System;
using System.Collections.Generic;
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
        public IActionResult All()
        {
            return Ok(_userService.GetAll());
        }
        
        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="userId">Guid</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [HttpGet("{userId:guid}")]
        public IActionResult Get(Guid userId)
        {
            try
            {
                return Ok(_userService.GetById(userId));
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
        public IActionResult Create([FromBody] UserModel userModel)
        {
            try
            {
                return Ok(_userService.Create(userModel));
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
        public IActionResult Update([FromBody] UserModel userModel)
        {
            try
            {
                return Ok(_userService.Update(userModel));
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
        public IActionResult Delete(Guid userId)
        {
            try
            {
                _userService.Delete(userId);
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
        /// <param name="password"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("{userId:guid}/set-password")]
        public IActionResult SetPassword(Guid userId, [FromBody] string password)
        {
            try
            {
                _userService.SetPassword(userId, password);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}