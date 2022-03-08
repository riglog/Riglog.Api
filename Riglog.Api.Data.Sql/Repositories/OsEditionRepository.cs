using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsEditionRepository : GenericRepository<OsEdition>, IOsEditionRepository
{
    private readonly AppDbContext _dbContext;
    public OsEditionRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OsEdition> GetByNameAsync(Guid osDistributionId, string name)
    {
        return await _dbContext.OsEditions.SingleAsync(s => s.Name == name && s.OsDistributionId == osDistributionId);
    }
}