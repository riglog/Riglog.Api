using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<List<UserModel>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetById(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<Guid> Create(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            user.Id = new Guid();
            await _userRepository.Create(user);
            return user.Id;
        }

        public async Task<Guid> Update(UserModel userModel)
        {
            var currentUser = await _userRepository.GetById(userModel.Id);
            var user = _mapper.Map(userModel, currentUser);
            user.UpdatedDate = DateTime.Now;
            await _userRepository.Update(user);
            return user.Id;
        }

        public async Task Delete(Guid userId) => await _userRepository.Delete(userId);
        
        public async Task SetPassword(Guid userId, string password)
        {
            var user = await _userRepository.GetById(userId);
            
            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, password);
                
            await _userRepository.Update(user);
        }
    }
}