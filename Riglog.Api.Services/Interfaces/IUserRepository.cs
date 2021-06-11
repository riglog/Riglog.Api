using Riglog.Api.Data.Entities;

namespace Riglog.Api.Services.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
    }
}