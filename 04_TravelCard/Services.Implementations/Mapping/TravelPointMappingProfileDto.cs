using AutoMapper;
using Domain.Entities;
using Services.Contracts.TravelPoint;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности точки остановки.
    /// </summary>
    public class TravelPointMappingProfileDto : Profile
    {
        public TravelPointMappingProfileDto()
        {
            CreateMap<TravelPoint, TravelPointDto>();

            CreateMap<CreatingTravelPointDto, TravelPoint>();

            CreateMap<UpdatingTravelPointDto, TravelPoint>()
                .ForMember(d => d.Deleted, map => map.Ignore());

        }
    }
}
