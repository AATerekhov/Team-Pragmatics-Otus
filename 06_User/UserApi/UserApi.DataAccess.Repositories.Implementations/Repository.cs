using Microsoft.EntityFrameworkCore;
using UserApi.DataAccess.Entities.Base;
using UserApi.DataAccess.EntityFramework;
using UserApi.DataAccess.Repositories.Abstractions;

namespace UserApi.DataAccess.Repositories.Implementations
{
    public class Repository<TEntity, TId>(DataContext context) : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId> 
        where TId : struct
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync() => (await context.Set<TEntity>().AsNoTracking().ToListAsync()).AsEnumerable();

        public async Task<TEntity?> GetByIdAsync(TId id) =>await context.Set<TEntity>().FindAsync(id).AsTask();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(TEntity entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return;
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity) =>await DeleteAsync(entity.Id);

    }
}
