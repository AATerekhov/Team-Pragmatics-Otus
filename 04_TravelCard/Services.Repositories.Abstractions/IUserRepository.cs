
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User, Guid>
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
