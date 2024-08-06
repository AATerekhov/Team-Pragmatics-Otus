using Services.Contracts.TravelPoint;
namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с точками остановок.
    /// </summary>
    public interface ITravelPointService
    {
        /// <summary>
        /// Получить Точку остановки по ID. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО точки оставки. </returns>
        Task<TravelPointDto> GetByIdAsync(int id);

        /// <summary>
        /// Получить все точки остановки по ID путешествия.
        /// </summary>
        /// <param name="travelId"></param>
        /// <returns></returns>
        Task<List<TravelPointDto>> GetAllByTravelIdAsync(int travelId);

        /// <summary>
        /// Создать Точку остановки.
        /// </summary>
        /// <param name="creatingTravelPointDto"> ДТО создания точки остановки. </param>
        /// <returns> Идентификатор. </returns>
        Task<int> CreateAsync(CreatingTravelPointDto creatingTravelPointDto);

        /// <summary>
        /// Изменить Точку остановки.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingTravelPointDto"> ДТО редактирования точки остановки. </param>
        Task UpdateAsync(int id, UpdatingTravelPointDto updatingTravelPointDto);

        /// <summary>
        /// Удалить Точку остановки.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Получить список точек остановки.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Страница точек остановки. </returns>
        //Task<ICollection<TravelPointDto>> GetPagedAsync(int page, int pageSize);
    }
}
