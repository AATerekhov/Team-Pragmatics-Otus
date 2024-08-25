using AutoMapper;
using Domain.Entities;
using Services.Contracts.PlaceType;
using Services.Contracts.Road;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    public class RoadMappingsProfile:Profile
    {
        public RoadMappingsProfile()
        {
            CreateMap< RoadDto, Road> ();
            CreateMap< RoadPointDto, RoadPoint> ();
        }
    }
}
