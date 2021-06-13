using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
    }
}