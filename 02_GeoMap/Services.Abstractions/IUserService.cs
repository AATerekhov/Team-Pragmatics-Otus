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
        void Create(CreatingUserDto creatingUserDto);

    }
}
