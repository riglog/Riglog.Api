using System;
using System.Threading.Tasks;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces;

public interface IOsVersionRepository : IGenericRepository<OsVersion>
{
    public Task<OsVersion> GetByNameAsync(Guid osDistributionId, string name);
}