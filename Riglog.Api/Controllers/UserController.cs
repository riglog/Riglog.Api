using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Riglog.Api.Data.Entities;
using Riglog.Api.Models;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// List all users
        /// </summary>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserModel>))]
        [HttpGet("all")]
        public IActionResult All()
        {
            var users = _userRepository.GetAll();
           
            return Ok(_mapper.Map<List<UserModel>>(users));
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
                var user = _userRepository.GetById(userId);
                return Ok(_mapper.Map<UserModel>(user));
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
            var user = _mapper.Map<User>(userModel);
            
            try
            {
                _userRepository.Create(user);
                return Ok(user.Id);
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
                var currentUser = _userRepository.GetById(userModel.Id);
                var user = _mapper.Map(userModel, currentUser);
                user.UpdatedDate = DateTime.Now;
                _userRepository.Update(user);
                return Ok(user.Id);
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
                _userRepository.Delete(userId);
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
                var user = _userRepository.GetById(userId);
            
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, password);
                
                _userRepository.Update(user);
                
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}