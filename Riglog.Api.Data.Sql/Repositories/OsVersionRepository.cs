using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsVersionRepository : GenericRepository<OsVersion>, IOsVersionRepository
{
    private readonly AppDbContext _dbContext;
    public OsVersionRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OsVersion> GetByNameAsync(Guid osDistributionId, string name)
    {
        return await _dbContext.OsVersions.SingleAsync(s => s.Name == name && s.OsDistributionId == osDistributionId);
    }
}