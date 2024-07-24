using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.GeometricModel
{
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Point GetPoint(RoadPoint roadPoint) { return new Point(roadPoint.Longitude, roadPoint.Latitude); }

        public static Point operator +(Point a, Vector b) { return new Point(a.X + b.X, a.Y + b.Y); }
        public static Point operator -(Point a, Vector b) { return new Point(a.X - b.X, a.Y - b.Y); }
    }
}
