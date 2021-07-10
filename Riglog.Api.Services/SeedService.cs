using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services
{
    public class SeedService : ISeedService
    {
        private readonly IUserRepository _userRepository;
        
        public SeedService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task SeedAdminUserAsync(string adminPassword)
        {
            const string adminUser = "riglogadmin";

            var guid = new Guid();
            
            var admin = new User
            {
                Id = guid,
                Username = adminUser,
                FirstName = "Riglog",
                LastName = "Admin",
                Email = "admin@riglog.cz",
                CreatedBy = guid,
                UpdatedBy = guid,
                IsSuperAdmin = true
            };
            await _userRepository.CreateAsync(admin);
            
            var hasher = new PasswordHasher<User>();
            admin.Password = hasher.HashPassword(admin, adminPassword);
            
            await _userRepository.UpdateAsync(admin);
        }
    }
}