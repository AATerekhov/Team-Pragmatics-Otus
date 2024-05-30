using AutoMapper;
using GeoMap.Model.Place;
using Services.Contracts.Place;

namespace GeoMap.Mapping
{
    public class PlaceMappingsProfile:Profile
    {
        public PlaceMappingsProfile()
        {
            CreateMap<PlaceDto, PlaceModel>();
            CreateMap<CreatingPlaceModel,  CreatingPlaceDto> ();
            CreateMap<UpdatingPlaceModel,  UpdatingPlaceDto> ();
        }
    }
}
