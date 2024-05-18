using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.Fuelling;
using Services.Contracts.Place;
using Services.Contracts.PlaceType;
using Services.Implementations.Exceptions;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class FuellingService : IFuellingService
    {
        public readonly IMapper _mapper;
        public readonly IFuellingRepository _fuellingRepository;

        public FuellingService(IMapper mapper, IFuellingRepository fuellingRepository)
        {
            _mapper = mapper;
            _fuellingRepository = fuellingRepository;
        }

        public async Task<Guid> CreateAsync(CreatingFuellingDto creatingFuellingDto)
        {
            var fuelling = _mapper.Map<CreatingFuellingDto, Fuelling>(creatingFuellingDto);
            var creatingFuelling = await _fuellingRepository.AddAsync(fuelling);
            await _fuellingRepository.SaveChangesAsync();
            return creatingFuelling.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fuelling = await _fuellingRepository.GetAsync(id, CancellationToken.None);
            if (fuelling == null)
                throw new FuellingNotException(id);
            await _fuellingRepository.DeleteAsync(fuelling);
        }

        public async Task<FuellingDto> GetByIdAsync(Guid id)
        {
            var fuelling = await _fuellingRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Fuelling, FuellingDto>(fuelling);
        }

        public async Task<ICollection<FuellingDto>> GetAllFuellingsAsync()
        {
            ICollection<Fuelling> entities = await _fuellingRepository.GetAllAsync(CancellationToken.None);
            return _mapper.Map<ICollection<Fuelling>, ICollection<FuellingDto>>(entities);
        }

        public async Task UpdateAsync(Guid id, UpdatingFuellingDto updatingFuellingDto)
        {
            var fuelling = await _fuellingRepository.GetAsync(id, CancellationToken.None);
            if (fuelling == null) throw new FuellingNotException(id);

            fuelling.Name = updatingFuellingDto.Name;
            fuelling.IsCafe = updatingFuellingDto.IsCafe;
            fuelling.IsFoodShop = updatingFuellingDto.IsFoodShop;
            await _fuellingRepository.UpdateAsync(fuelling);
        }
    }
}
