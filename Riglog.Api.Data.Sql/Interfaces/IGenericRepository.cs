using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riglog.Api.Data.Sql.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<List<TEntity>> GetAllAsync();

    public Task<TEntity> GetByIdAsync(Guid id);

    public Task CreateAsync(TEntity entity);

    public Task UpdateAsync(TEntity entity);

    public Task DeleteAsync(Guid id);
}