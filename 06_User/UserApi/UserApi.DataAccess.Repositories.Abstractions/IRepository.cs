using UserApi.DataAccess.Entities.Base;

namespace UserApi.DataAccess.Repositories.Abstractions
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId> where TId : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TId id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
        Task DeleteAsync(TEntity entity);
    }
}
