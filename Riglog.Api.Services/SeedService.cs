using System;
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
        
        public void SeedAdminUser(string adminPassword)
        {
            const string adminUser = "riglogadmin";

            var admin = new User
            {
                Username = adminUser,
                FirstName = "Riglog",
                LastName = "Admin",
                Email = "admin@riglog.cz",
                CreatedBy = adminUser,
                UpdatedBy = adminUser,
                IsSuperAdmin = true
            };
            _userRepository.Create(admin);
            
            var hasher = new PasswordHasher<User>();
            admin.Password = hasher.HashPassword(admin, adminPassword);
            
            _userRepository.Update(admin);
        }
    }
}