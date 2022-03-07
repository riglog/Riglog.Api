using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsDistributionRepository : GenericRepository<OsDistribution>, IOsDistributionRepository
{
    public OsDistributionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}