using System;
using System.Collections.Generic;
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
        
        public List<UserModel> GetAll()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<List<UserModel>>(users);
        }

        public UserModel GetById(Guid userId)
        {
            var user = _userRepository.GetById(userId);
            return _mapper.Map<UserModel>(user);
        }

        public Guid Create(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            user.Id = new Guid();
            _userRepository.Create(user);
            return user.Id;
        }

        public Guid Update(UserModel userModel)
        {
            var currentUser = _userRepository.GetById(userModel.Id);
            var user = _mapper.Map(userModel, currentUser);
            user.UpdatedDate = DateTime.Now;
            _userRepository.Update(user);
            return user.Id;
        }

        public void Delete(Guid userId) => _userRepository.Delete(userId);
        
        public void SetPassword(Guid userId, string password)
        {
            var user = _userRepository.GetById(userId);
            
            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, password);
                
            _userRepository.Update(user);
        }
    }
}