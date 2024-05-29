using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) 
        {

        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Курс. </returns>
        public override async Task<User> GetAsync(int id, CancellationToken cancellationToken)
        {
            //return await Context.Set<User>().AsNoTracking().ToListAsync().AsEnumerable();
            var query = Context.Set<User>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
            //return await query.SingleOrDefaultAsync(cancellationToken);
        }

        //public Task<User?> GetUserByIdAsync(int id) => Context.Set<User>().FindAsync(id).AsTask();

        /// <summary>
        /// Получить список всех объектов.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список уроков. </returns>
        public async Task<List<User>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll().Where(l => !l.Deleted);
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }
    }
}
