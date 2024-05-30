using AutoMapper;
using Domain.Entities;
using Services.Contracts.PlaceType;
using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    public class PlaceTypeMappingsProfile:Profile
    {
        public PlaceTypeMappingsProfile()
        {
            CreateMap<PlaceType, PlaceTypeDto>();
            CreateMap<CreatingPlaceTypeDto, PlaceType>();
            CreateMap<UpdatingPlaceTypeDto, PlaceType>();
            CreateMap<PlaceTypeDto , PlaceType>();
        }
    }
}
