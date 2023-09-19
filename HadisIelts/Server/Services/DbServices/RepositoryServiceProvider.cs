using HadisIelts.Server.Data;
using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Services.DbServices
{
    public class RepositoryServiceProvider<TEntity, TKey> : ICustomRepositoryServices<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        public RepositoryServiceProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Delete(TEntity entity)
        {
            _dbContext.Remove<TEntity>(entity);
            var result = _dbContext.SaveChanges();
            return result > 0 ? true : false;
        }

        public async Task<TEntity?> FindByIDAsync(TKey id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            var data = _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return data.Entity;
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Update<TEntity>(entity);
            var result = _dbContext.SaveChanges();
            return result > 0 ? true : false;
        }

        public bool UpdateAll(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Update(entity);
            }
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
