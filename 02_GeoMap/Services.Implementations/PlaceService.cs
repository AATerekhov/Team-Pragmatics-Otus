﻿using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
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
    public class PlaceService:IPlaceService
    {
        public readonly IMapper _mapper;
        public readonly IPlaceRepository _placeRepository;
        public PlaceService(IMapper mapper, IPlaceRepository placeRepository)
        {
            _mapper = mapper;
            _placeRepository = placeRepository;
        }

        public async Task<Guid> CreateAsync(CreatingPlaceDto creatingPlaceDto)
        {
            var place = _mapper.Map<CreatingPlaceDto, Place>(creatingPlaceDto);
            place.PlaceTypeID = creatingPlaceDto.PlaceTypeId;
            var createdLesson = await _placeRepository.AddAsync(place);
            await _placeRepository.SaveChangesAsync();

            return createdLesson.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var placeType = await _placeRepository.GetAsync(id, CancellationToken.None);
            if (placeType == null)
                throw new PlaceNotExistException(id);
            await _placeRepository.DeleteAsync(placeType);
        }

        public async Task<PlaceDto> GetByIdAsync(Guid id)
        {
            var place = await _placeRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Place, PlaceDto>(place);
        }

        public async Task<ICollection<PlaceDto>> GetPlaceForTypeAsync(int placeTypeId,PlaceTypeDto placeTypeDto)
        {
            ICollection<Place> entities = await _placeRepository.GetForTypeAsync(placeTypeId);
            return _mapper.Map<ICollection<Place>, ICollection<PlaceDto>>(entities);
        }

        public async Task UpdateAsync(Guid id, UpdatingPlaceDto updatingPlaceDto)
        {
            var place = await _placeRepository.GetAsync(id, CancellationToken.None);
            if (place == null)
                throw new PlaceNotExistException(id);

            place.Name = updatingPlaceDto.Name;
            place.Description = updatingPlaceDto.Description;
            await _placeRepository.UpdateAsync(place);
        }
    }
}