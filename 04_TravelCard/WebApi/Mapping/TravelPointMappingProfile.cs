using AutoMapper;
using Services.Contracts.TravelPoint;

namespace WebApi.Mapping
{
    public class TravelPointMappingProfile : Profile
    {
        public TravelPointMappingProfile()
        {
            CreateMap<TravelPointDto, TravelPointModel>();
            CreateMap<CreatingTravelPointModel, CreatingTravelPointDto>();
            CreateMap<UpdatingTravelPointModel, UpdatingTravelPointDto>();
        }
    }
}
