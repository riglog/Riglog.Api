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
        
        public async Task SeedAdminUser(string adminPassword)
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
            await _userRepository.Create(admin);
            
            var hasher = new PasswordHasher<User>();
            admin.Password = hasher.HashPassword(admin, adminPassword);
            
            await _userRepository.Update(admin);
        }
    }
}