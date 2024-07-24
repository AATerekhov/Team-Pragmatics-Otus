using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.Extentions
{
    public static class EntityExtentions
    {
        public async static Task<bool> OnTheRoad(this Road road, Place place)
        {
            var point = place.ConvertInPoint();

            return true;
        }

        public static RoadPoint ConvertInPoint(this Place place) => new RoadPoint() { Latitude = place.Latitude, Longitude = place.Longitude };
        public static Vector GetVector(RoadPoint roadPoint) => new Vector2((float)roadPoint.Longitude, (float)roadPoint.Latitude);
    }
}
