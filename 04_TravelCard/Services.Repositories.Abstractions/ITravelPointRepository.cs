using Domain.Entities;
namespace Services.Repositories.Abstractions
{
    public interface ITravelPointRepository : IRepository<TravelPoint, int>
    {
        /// <summary>
        /// Получить список точек остановок.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список точек остановок. </returns>
        Task<List<TravelPoint>> GetPagedAsync(int page, int itemsPerPage);

        /// <summary>
        /// Получить все точки остановки по ID путешествия.
        /// </summary>
        /// <param name="travelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TravelPoint>> GetAllByTravelIdAsync(int travelId, CancellationToken cancellationToken);
    }
}
