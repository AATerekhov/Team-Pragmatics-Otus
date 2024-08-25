using AutoMapper;
using GeoMap.Model.Place;
using GeoMap.Model.Road;
using Services.Contracts.Place;
using Services.Contracts.Road;

namespace GeoMap.Mapping
{
    public class RoadMappingProfile : Profile
    {
        public RoadMappingProfile()
        {
            CreateMap<RoadModel,RoadDto>();
            CreateMap<RoadPointModel, RoadPointDto>();
        }
    }
}
