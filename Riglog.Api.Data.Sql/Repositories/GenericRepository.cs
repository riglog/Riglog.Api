using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;

    protected GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<TEntity>();
    }
        
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _entities.Where(s => s.IsDeleted == false).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _entities.SingleAsync(s => s.Id == id);
    }

    public async Task<Guid> CreateAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _entities.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _entities.SingleAsync(s => s.Id == id);

        entity.IsDeleted = true;
        _entities.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}