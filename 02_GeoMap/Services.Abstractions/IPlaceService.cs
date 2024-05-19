using Services.Contracts.Place;
using Services.Contracts.PlaceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса мест посещения.
    /// </summary>
    public interface IPlaceService
    {
        /// <summary>
        /// Создать место посещения.
        /// </summary>
        /// <param name="creatingPlaceDto"> ДТО места посещения.</param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingPlaceDto creatingPlaceDto);
        Task<PlaceDto> CreatePlaceAsync(CreatingPlaceDto place);

        /// <summary>
        /// Получить тип мест посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО места посещения. </returns>
        Task<PlaceDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Изменить место посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingPlaceDto"> ДТО места посещения. </param>
        Task UpdateAsync(Guid id, UpdatingPlaceDto updatingPlaceDto);

        /// <summary>
        /// Удалить место посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить коллекцию мест посещения по типу.
        /// </summary>
        /// <param name="placeTypeDto">Тип мест посещения.</param>
        /// <returns>Коллекция мест посещения.</returns>
        Task<ICollection<PlaceDto>> GetPlaceForTypeAsync(int placeTypeId);

    }
}
