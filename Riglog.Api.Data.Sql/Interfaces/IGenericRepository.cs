using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riglog.Api.Data.Sql.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetAll();

        public Task<TEntity> GetById(Guid id);

        public Task Create(TEntity entity);

        public Task Update(TEntity entity);

        public Task Delete(Guid id);
    }
}