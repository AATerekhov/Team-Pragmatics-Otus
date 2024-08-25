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
            var point = place.ConvertByRoadPoint().Geometry();
            var polyline = road.Geometry();
            foreach (var section in polyline.Sections)
            {
                var poligon = section.Offset(road.Offset);
                if(poligon.CheckInsertPoint(point)) return true;
            }
            return false;
        }

        public static ABCD Borders(this Road road) => road.Geometry().GetABCD();
        public static RoadPoint ConvertByRoadPoint(this Place place) => new RoadPoint() { Latitude = place.Latitude, Longitude = place.Longitude };
        public static Point Geometry(this RoadPoint point ) => new Point(point.Longitude , point.Latitude);
        public static Polyline Geometry(this Road road) => new Polyline(road.RoadPoints.Select(p => p.Geometry()).ToList());
    }
}
