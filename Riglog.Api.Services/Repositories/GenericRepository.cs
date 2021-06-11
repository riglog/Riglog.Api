using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services.Repositories
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
        
        public IQueryable<TEntity> GetAll()
        {
            return _entities.Where(s => s.IsDeleted == false);
        }

        public TEntity GetById(Guid id)
        {
            return _entities.Single(s => s.Id == id);
        }

        public void Create(TEntity entity)
        {
            _entities.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _entities.Single(s => s.Id == id);
            
            if (entity == null) return;
            
            entity.IsDeleted = true;
            _entities.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}