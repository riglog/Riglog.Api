using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;
using Riglog.Api.Services.Interfaces;
using Riglog.Api.Services.Models;

namespace Riglog.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<Guid> CreateAsync(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            user.Id = new Guid();
            
            var jwtGuid = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.PrimarySid)?.Value;

            if (jwtGuid != null)
            {
                var currentUserGuid = new Guid(jwtGuid);
                user.CreatedBy = currentUserGuid;
                user.UpdatedBy = currentUserGuid;
            }
            await _userRepository.CreateAsync(user);
            return user.Id;
        }

        public async Task<Guid> UpdateAsync(UserModel userModel)
        {
            var currentUser = await _userRepository.GetByIdAsync(userModel.Id);
            var user = _mapper.Map(userModel, currentUser);
            
            var jwtGuid = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.PrimarySid)?.Value;

            if (jwtGuid != null)
            {
                var currentUserGuid = new Guid(jwtGuid);
                user.UpdatedBy = currentUserGuid;
            }
            user.UpdatedDate = DateTime.Now;
            await _userRepository.UpdateAsync(user);
            return user.Id;
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }
        
        public async Task SetPasswordAsync(Guid userId, string password)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            
            var jwtGuid = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.PrimarySid)?.Value;

            if (jwtGuid != null)
            {
                var currentUserGuid = new Guid(jwtGuid);
                user.UpdatedBy = currentUserGuid;
            }
            user.UpdatedDate = DateTime.Now;
            
            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, password);
                
            await _userRepository.UpdateAsync(user);
        }
    }
}