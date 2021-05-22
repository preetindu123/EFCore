using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void DeleteById(object Id)
        {
            var entity = _dbContext.Set<TEntity>().Find(Id);
            if (entity != null)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        public TEntity FindById(object Id)
        {
            return _dbContext.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
           return _dbContext.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);

        }
    }
}
