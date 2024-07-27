using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using GeometryMap.GeometricModel;
using GeometryMap.GeometricModel.Figure;


namespace Infrastructure.Repositories.Implementations.Extentions
{
    public static class EntityExtentions
    {
        public static  bool OnTheRoad(this Road road, Place place)
        {
            //double offset = 0.044444;
            var roadPoint = place.ConvertByRoadPoint();
            var polygon = road.Geometry();
            return polygon.CheckInsertPoint(roadPoint.Geometry());
        }

        public static RoadPoint ConvertByRoadPoint(this Place place) => new RoadPoint() { Latitude = place.Latitude, Longitude = place.Longitude };
        public static Point Geometry(this RoadPoint point ) => new Point( point.Latitude, point.Longitude );
        public static Polygon Geometry(this Road road) => new Section(road.Start.Geometry(), road.Finish.Geometry()).Offset(road.Offset);
    }
}
