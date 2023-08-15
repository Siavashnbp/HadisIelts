using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Services.DbServices
{
    public interface ICustomRepositoryServices<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> FindByIDAsync(TKey id);
        List<TEntity> GetAll();
        bool Delete(TEntity entity);
        Task<TKey> InsertAsync(TEntity entity);
        bool Update(TEntity entity);
    }
}
