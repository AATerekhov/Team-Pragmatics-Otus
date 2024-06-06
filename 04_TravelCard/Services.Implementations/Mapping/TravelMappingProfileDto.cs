using AutoMapper;
using Domain.Entities;
using Services.Contracts.Travel;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности путешествия.
    /// </summary>
    public class TravelMappingProfileDto : Profile
    {
        public TravelMappingProfileDto()
        {
            CreateMap<Travel, TravelDto>();

            CreateMap<CreatingTravelDto, Travel>();

            CreateMap<UpdatingTravelDto, Travel>()
                .ForMember(d => d.TravelDesc, map => map.Ignore());

        }
    }
}
