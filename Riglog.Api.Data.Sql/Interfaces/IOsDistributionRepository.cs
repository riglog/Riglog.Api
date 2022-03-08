using System;
using System.Threading.Tasks;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces;

public interface IOsDistributionRepository : IGenericRepository<OsDistribution>
{
    public Task<OsDistribution> GetByNameAsync(Guid osFamilyId, string name);
}