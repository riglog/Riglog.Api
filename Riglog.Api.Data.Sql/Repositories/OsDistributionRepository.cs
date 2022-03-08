using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsDistributionRepository : GenericRepository<OsDistribution>, IOsDistributionRepository
{
    private readonly AppDbContext _dbContext;
    public OsDistributionRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OsDistribution> GetByNameAsync(Guid osFamilyId, string name)
    {
        return await _dbContext.OsDistributions.SingleAsync(s => s.Name == name && s.OsFamilyId == osFamilyId);
    }
}