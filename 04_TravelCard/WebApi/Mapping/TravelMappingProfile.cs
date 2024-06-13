using AutoMapper;
using Services.Contracts.Travel;

namespace WebApi.Mapping
{
    public class TravelMappingProfile : Profile
    {
        public TravelMappingProfile()
        {
            CreateMap<TravelDto, TravelModel>();
            CreateMap<CreatingTravelModel, CreatingTravelDto>();
            CreateMap<UpdatingTravelModel, UpdatingTravelDto>();
        }
    }
}
