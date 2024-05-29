
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    public interface ITravelRepository : IRepository<User, int>
    {
        /// <summary>
        /// Получить список юзеров.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список юзеров. </returns>
        Task<List<User>> GetPagedAsync(int page, int itemsPerPage);
    }
}
