using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Riglog.Api.Services.Models;

namespace Riglog.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserModel>> GetAllAsync();

        public Task<UserModel> GetByIdAsync(Guid userId);

        public Task<Guid> CreateAsync(UserModel userModel);
        
        public Task<Guid> UpdateAsync(UserModel userModel);
        
        public Task DeleteAsync(Guid userId);

        public Task SetPasswordAsync(Guid userId, string password);
    }
}