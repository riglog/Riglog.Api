using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _entities;

        protected GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }
        
        public async Task<List<TEntity>> GetAll()
        {
            return await _entities.Where(s => s.IsDeleted == false).ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _entities.SingleAsync(s => s.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = _entities.Single(s => s.Id == id);
            
            if (entity == null) return;
            
            entity.IsDeleted = true;
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}