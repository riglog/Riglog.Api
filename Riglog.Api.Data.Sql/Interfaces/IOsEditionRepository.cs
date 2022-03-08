using System;
using System.Threading.Tasks;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces;

public interface IOsEditionRepository : IGenericRepository<OsEdition>
{
    public Task<OsEdition> GetByNameAsync(Guid osDistributionId, string name);
}