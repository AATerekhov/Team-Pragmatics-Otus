using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public class TravelPointRepository : Repository<TravelPoint, int>, ITravelPointRepository
    {
        public TravelPointRepository(DataContext context) : base(context) 
        {

        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Точка остановки. </returns>
        public override async Task<TravelPoint> GetAsync(int id, CancellationToken cancellationToken)
        {
            var query = Context.Set<TravelPoint>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить все точки остановки по ID путешествия.
        /// </summary>
        /// <param name="travelId"> Id путешествия. </param>
        /// <returns> масств точек остановки. </returns>
        public async Task<List<TravelPoint>> GetAllByTravelIdAsync(int travelId, CancellationToken cancellationToken)
        {
            return await GetAll().Where(tp => tp.TravelId == travelId).ToListAsync();
        }

        //public Task<TravelPoint?> GetTravelPointByIdAsync(int id) => Context.Set<TravelPoint>().FindAsync(id).AsTask();

        /// <summary>
        /// Получить список всех объектов.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список точек остановки. </returns>
        public async Task<List<TravelPoint>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }
    }
}
