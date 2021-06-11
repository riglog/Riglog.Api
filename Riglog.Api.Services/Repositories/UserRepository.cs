using System.Linq;
using Riglog.Api.Data;
using Riglog.Api.Data.Entities;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public User GetByUsername(string username)
        {
            return _dbContext.Users.Single(q => q.Username == username);
        }
    }
}