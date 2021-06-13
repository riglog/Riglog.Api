using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services
{
    public class DbService : IDbService
    {
        private readonly AppDbContext _dbContext;

        public DbService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<string>> GetMigrationsAsync()
        {
            return await _dbContext.Database.GetPendingMigrationsAsync();
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}