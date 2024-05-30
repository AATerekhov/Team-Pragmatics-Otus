using AutoMapper;
using Domain.Entities;
using Services.Contracts.Fuelling;
using Services.Contracts.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    public class FuellingMappingsProfile:Profile
    {
        public FuellingMappingsProfile()
        {
            CreateMap<Fuelling , FuellingDto>();

            CreateMap<CreatingFuellingDto, Fuelling>();
            //.ForMember(d => d.Id, map => map.Ignore())
            //.ForMember(d => d.Name, map => map.Ignore())
            //.ForMember(d => d.IsCafe, map => map.Ignore())
            //.ForMember(d => d.IsFoodShop, map => map.Ignore())
            //.ForMember(d => d.Deleted, map => map.Ignore());

            CreateMap<UpdatingFuellingDto, Fuelling>();
            //.ForMember(d => d.Id, map => map.Ignore())
            //.ForMember(d => d.Latitude, map => map.Ignore())
            //.ForMember(d => d.Longitude, map => map.Ignore())
            //.ForMember(d => d.Deleted, map => map.Ignore());

            CreateMap<FuellingDto,  Fuelling> ();
        }
    }
}
