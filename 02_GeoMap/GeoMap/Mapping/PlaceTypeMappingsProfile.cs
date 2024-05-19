using AutoMapper;
using GeoMap.Model.PlaceType;
using Services.Contracts.PlaceType;

namespace GeoMap.Mapping
{
    public class PlaceTypeMappingsProfile : Profile
    {
        public PlaceTypeMappingsProfile()
        {
            CreateMap<PlaceTypeDto, PlaceTypeModel>();
            CreateMap<CreatingPlaceTypeModel,  CreatingPlaceTypeDto> ();
            CreateMap<UpdatingPlaceTypeModel,  UpdatingPlaceTypeDto> ();
        }
    }
}
