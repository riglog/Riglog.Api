using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsVersionRepository : GenericRepository<OsVersion>, IOsVersionRepository
{
    public OsVersionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}