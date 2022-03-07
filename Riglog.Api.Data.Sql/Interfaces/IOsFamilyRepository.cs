using System.Threading.Tasks;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces;

public interface IOsFamilyRepository : IGenericRepository<OsFamily>
{
    public Task<OsFamily> GetByNameAsync(string name);
}