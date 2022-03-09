using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class ComputerTypeRepository : GenericRepository<ComputerType>, IComputerTypeRepository
{
    private readonly AppDbContext _dbContext;
    
    public ComputerTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ComputerType> GetByNameAsync(string name)
    {
        return await _dbContext.ComputerTypes.SingleAsync(s => s.Name == name);
    }
}