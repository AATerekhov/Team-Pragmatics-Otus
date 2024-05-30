using Services.Contracts.Fuelling;
using Services.Contracts.Place;
using Services.Contracts.PlaceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IFuellingService
    {
        /// <summary>
        /// Создать заправочную станцию.
        /// </summary>
        /// <param name="creatingPlaceDto"> ДТО заправочная станция.</param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingFuellingDto creatingFuellingDto);

        Task<FuellingDto> CreateFuellingAsync(CreatingFuellingDto creatingFuellingDto);
        /// <summary>
        ///  Получить заправочную станцию.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FuellingDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Изменить заправочную станцию.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingPlaceDto"> ДТО места посещения. </param>
        Task UpdateAsync(Guid id, UpdatingFuellingDto updatingFuellingDto);

        /// <summary>
        /// Удалить заправочную станцию.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить коллекцию заправочных станций.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<FuellingDto>> GetAllFuellingsAsync();

        
    }
}
