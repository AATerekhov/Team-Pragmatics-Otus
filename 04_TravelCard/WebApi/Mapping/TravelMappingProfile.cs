using AutoMapper;
using Services.Contracts.Travel;
using Services.Contracts.User;

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
