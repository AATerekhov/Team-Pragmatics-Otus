using Services.Contracts;
using Services.Contracts.Travel;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с путешествиями.
    /// </summary>
    public interface ITravelService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TravelDto>> GetTravelsAsync();

        /// <summary>
        /// Получить путешествие. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО путешествия. </returns>
        Task<TravelDto> GetByIdAsync(int id);

        /// <summary>
        /// Получить путешествия по Guid User'a. 
        /// </summary>
        /// <param name="userId"> Идентификатор юзера. </param>
        /// <returns> коллекцию ДТО путешествий. </returns>
        Task<IEnumerable<TravelDto>> GetByUserIdAsync(Guid userId);

        //Task<TravelDto?> GetTravelAsync(int id);

        /// <summary>
        /// Создать путешествие.
        /// </summary>
        /// <param name="CreatingTravelDto"> ДТО создания путешествия. </param>
        /// <returns> Идентификатор. </returns>
        Task<TravelDto> CreateAsync(CreatingTravelDto creatingTravelDto);

        /// <summary>
        /// Изменить путешествие.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="UpdatingTravelDto"> ДТО редактирования путешествия. </param>
        Task UpdateAsync(int id, UpdatingTravelDto updatingTravelDto);

        /// <summary>
        /// Удалить путешествие.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Получить список путешествий.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Страница путешествий. </returns>
        //Task<ICollection<TravelDto>> GetPagedAsync(int page, int pageSize);
    }
}
