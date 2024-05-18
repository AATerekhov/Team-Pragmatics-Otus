using Services.Contracts.PlaceType;
using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса типов мест посещения.
    /// </summary>
    public interface IPlaceTypeService
    {
        /// <summary>
        /// Создать тип мест посещения.
        /// </summary>
        /// <param name="creatingPlaceTypeDto"> ДТО типа места посещения.</param>
        /// <returns> Идентификатор. </returns>
        Task<int> CreateAsync(CreatingPlaceTypeDto creatingPlaceTypeDto);

        /// <summary>
        /// Получить тип мест посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО типа мест посещения. </returns>
        Task<PlaceTypeDto> GetByIdAsync(int id);

        /// <summary>
        /// Изменить тип мест посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingPlaceType"> ДТО типа мест посещения. </param>
        Task UpdateAsync(int id, UpdatingPlaceTypeDto updatingPlaceType);

        /// <summary>
        /// Удалить тип мест посещения.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Получить все типы мест посещения.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<PlaceTypeDto>> GetAllTypesAsync();
    }
}
