using AutoMapper;
using Domain.Entities;
using Services.Contracts.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    public class PlaceMappingsProfile : Profile
    {
        public PlaceMappingsProfile()
        {
            CreateMap<Place, PlaceDto>();

            CreateMap<CreatingPlaceDto, Place>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.PlaceTypeID, map => map.Ignore())
                .ForMember(d => d.PlaceType, map => map.Ignore())
                .ForMember(d => d.Description, map => map.Ignore());

            CreateMap<UpdatingPlaceDto, Place>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.PlaceTypeID, map => map.Ignore())
                .ForMember(d => d.PlaceType, map => map.Ignore())
                .ForMember(d => d.Latitude, map => map.Ignore())
                .ForMember(d => d.Longitude, map => map.Ignore());
        }
    }
}
