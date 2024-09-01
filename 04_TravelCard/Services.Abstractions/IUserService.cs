using Services.Contracts;
using Services.Contracts.User;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с юзерами.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить всех юзеров
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetUsersAsync();

        /// <summary>
        /// Получить юзера. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО юзера. </returns>
        Task<UserDto> GetByIdAsync(Guid id);

        //Task<UserDto?> GetUserAsync(int id);

        /// <summary>
        /// Создать юзера.
        /// </summary>
        /// <param name="CreatingUserDto"> ДТО создания юзера. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingUserDto creatingUserDto);

        /// <summary>
        /// Изменить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="UpdatingUserDto"> ДТО редактирования юзера. </param>
        Task UpdateAsync(Guid id, UpdatingUserDto creatingUserDto);

        /// <summary>
        /// Удалить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить список юзеров.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Страница юзеров. </returns>
        //Task<ICollection<UserDto>> GetPagedAsync(int page, int pageSize);
    }
}
