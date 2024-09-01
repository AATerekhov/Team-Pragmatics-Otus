using Services.Contracts.Place;
using Services.Contracts.User;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с польователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="creatingUserDto"> ДТО пользователя. </param>
        /// <returns> Идентификатор. </returns>
        Task CreateAsync(CreatingUserDto creatingUserDto);

        /// <summary>
        /// Получить тип мест посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО места посещения. </returns>
        Task<UserDto> GetByIdAsync(Guid id);

    }
}
