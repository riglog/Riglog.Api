using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Riglog.Api.Services.Models;

namespace Riglog.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserModel>> GetAll();

        public Task<UserModel> GetById(Guid userId);

        public Task<Guid> Create(UserModel userModel);
        
        public Task<Guid> Update(UserModel userModel);
        
        public Task Delete(Guid userId);

        public Task SetPassword(Guid userId, string password);
    }
}