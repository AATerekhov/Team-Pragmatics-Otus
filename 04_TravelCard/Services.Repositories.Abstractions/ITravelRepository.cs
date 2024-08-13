
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    public interface ITravelRepository : IRepository<Travel, int>
    {
        /// <summary>
        /// Получить список путешествий.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список путешествий. </returns>
        Task<List<Travel>> GetPagedAsync(int page, int itemsPerPage);
        Task<IEnumerable<Travel>> GetAllActualAsync();
    }
}
