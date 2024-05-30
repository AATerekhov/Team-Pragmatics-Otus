using AutoMapper;
using GeoMap.Model.Fuelling;
using GeoMap.Model.User;
using Services.Contracts.Fuelling;
using Services.Contracts.User;

namespace GeoMap.Mapping
{
    public class FuellingMappingsProfile:Profile
    {
        public FuellingMappingsProfile()
        {
            CreateMap<FuellingDto, FuellingModel>();
            CreateMap<CreatingFuellingModel,  CreatingFuellingDto> ();
            CreateMap<UpdatingFuellingModel,  UpdatingFuellingDto> ();
        }
    }
}
