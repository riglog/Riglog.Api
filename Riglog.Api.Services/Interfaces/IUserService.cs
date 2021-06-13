using System;
using System.Collections.Generic;
using Riglog.Api.Services.Models;

namespace Riglog.Api.Services.Interfaces
{
    public interface IUserService
    {
        public List<UserModel> GetAll();

        public UserModel GetById(Guid userId);

        public Guid Create(UserModel userModel);
        
        public Guid Update(UserModel userModel);
        
        public void Delete(Guid userId);

        public void SetPassword(Guid userId, string password);
    }
}