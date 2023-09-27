using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Services.DbServices
{
    public interface ICustomRepositoryServices<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<TEntity?> FindByIdAsync(TKey Id);
        List<TEntity> GetAll();
        bool Delete(TEntity entity);
        TEntity Insert(TEntity entity);
        bool Update(TEntity entity);
        bool UpdateAll(List<TEntity> entities);
    }
}
