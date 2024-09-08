using Domain.Entities;
using GeometryMap.GeometricModel.Figure;
using Infrastructure.Repositories.Implementations.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.TrasingFactiryMethods
{
    public class TrasingHighway: ITrasing
    {
        public int PlaceTipeId { get; private set; }
        private Road _road;        
        
        public TrasingHighway(Road road, int placeTipeId)
        {
            _road = road;
            PlaceTipeId = placeTipeId;
        }

        public ABCD GetBoard()
        {
            return _road.Borders().Offset(_road.Offset);
        }

        public bool OnTheRoad(Place place)
        {
            var point = place.ConvertByRoadPoint().Geometry();
            var polyline = _road.Geometry();
            foreach (var section in polyline.Sections)
            {
                var poligon = section.Offset(_road.Offset);
                if (poligon.CheckInsertPoint(point)) return true;
            }
            return false;
        }
    }
}
