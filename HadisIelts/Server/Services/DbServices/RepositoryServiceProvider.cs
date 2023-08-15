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

        public async Task<TKey> InsertAsync(TEntity entity)
        {
            var data = await _dbContext.AddAsync<TEntity>(entity);
            _dbContext.SaveChanges();
            return data.Entity.ID;
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Update<TEntity>(entity);
            var result = _dbContext.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
