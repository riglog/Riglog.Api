using System.Threading.Tasks;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByUsername(string username);
    }
}