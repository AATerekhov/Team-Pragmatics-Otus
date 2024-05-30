using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.PlaceType;
using Services.Contracts.User;
using Services.Implementations.Exceptions;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PlaceTypeService : IPlaceTypeService
    {
        private readonly IMapper _mapper;
        private readonly IPlaceTypeRepository _placeTypeRepository;
        public PlaceTypeService(IPlaceTypeRepository placeTypeRepository, IMapper mapper)
        {
            _placeTypeRepository = placeTypeRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(CreatingPlaceTypeDto creatingPlaceTypeDto)
        {
            var placeType = _mapper.Map<CreatingPlaceTypeDto, PlaceType>(creatingPlaceTypeDto);
            var createdPlaceType = await _placeTypeRepository.AddAsync(placeType);
            await _placeTypeRepository.SaveChangesAsync();

            return createdPlaceType.Id;
        }

        public async Task<PlaceTypeDto> GetByIdAsync(int id)
        {
            var placeType = await _placeTypeRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<PlaceType, PlaceTypeDto>(placeType);
        }

        public async Task<ICollection<PlaceTypeDto>> GetAllTypesAsync()
        {
            ICollection<PlaceType> entities = await _placeTypeRepository.GetAllAsync(CancellationToken.None);
            return _mapper.Map<ICollection<PlaceType>, ICollection<PlaceTypeDto>>(entities);
        }
        public async Task UpdateAsync(int id, UpdatingPlaceTypeDto updatingPlaceType)
        {
            var placeType = await _placeTypeRepository.GetAsync(id, CancellationToken.None);
            if (placeType == null)
                throw new PlaceTypeNotExistException(id);

            placeType.Description = updatingPlaceType.Description;
            await _placeTypeRepository.UpdateAsync(placeType);
        }
        public async Task DeleteAsync(int id)
        {
            var placeType = await _placeTypeRepository.GetAsync(id, CancellationToken.None);
            if (placeType == null)
                throw new PlaceTypeNotExistException(id);
            await _placeTypeRepository.DeleteAsync(placeType);
        }

        public async Task<PlaceTypeDto> CreatePlaceTypeAsync(CreatingPlaceTypeDto placeTypeDto)
        {
            var placeType = await _placeTypeRepository.AddAsync(_mapper.Map<PlaceType>(placeTypeDto));
            
            return _mapper.Map<PlaceTypeDto>(placeType);
        }
    }
}
