using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public class TravelRepository : Repository<Travel, int>, ITravelRepository
    {
        public TravelRepository(DataContext context) : base(context) 
        {

        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Путешествие. </returns>
        public override async Task<Travel> GetAsync(int id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Travel>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
            //return await query.SingleOrDefaultAsync(cancellationToken);
        }

        //public Task<Travel?> GetTravelByIdAsync(int id) => Context.Set<Travel>().FindAsync(id).AsTask();

        /// <summary>
        /// Получить список всех объектов.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список путешествий. </returns>
        public async Task<List<Travel>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Получить все путешествия
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Travel>> GetAllActualAsync() 
        {
            var result = await Context.Set<Travel>().Where(t => !t.Deleted).ToListAsync();
            return await Task.FromResult(result);
        }
    }
}
