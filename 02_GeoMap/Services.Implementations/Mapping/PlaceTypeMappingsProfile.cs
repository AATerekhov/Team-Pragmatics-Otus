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
                //.ForMember(d => d.Id, map => map.Ignore());

            CreateMap<UpdatingPlaceTypeDto, PlaceType>();
            //.ForMember(d => d.Id, map => map.Ignore())
            //.ForMember(d => d.Name, map => map.Ignore());

            CreateMap<PlaceTypeDto , PlaceType>();
        }
    }
}
