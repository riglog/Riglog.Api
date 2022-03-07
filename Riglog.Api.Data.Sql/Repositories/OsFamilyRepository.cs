using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsFamilyRepository : GenericRepository<OsFamily>, IOsFamilyRepository
{
    private readonly AppDbContext _dbContext;
    
    public OsFamilyRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<OsFamily> GetByNameAsync(string name)
    {
        return await _dbContext.OsFamilies.SingleAsync(s => s.Name == name);
    }
}