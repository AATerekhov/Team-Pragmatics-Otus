using GeometryMap.GeometricModel.Figure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel
{
    public struct Point
    {
        public double X;
        public double Y;
        public Vector GetVector() { return new Vector(X, Y); }
        public Point(double x, double y)
        {
            X = Math.Round(x, 6);
            Y = Math.Round(y, 6);
        }
        public static Point operator +(Point a, Vector b) { return new Point(a.X + b.X, a.Y + b.Y); }
        public static Point operator -(Point a, Vector b) { return new Point(a.X - b.X, a.Y - b.Y); }
        public static Vector operator -(Point a, Point b) { return new Vector(a.X - b.X, a.Y - b.Y); }
        public double Distance(Point other) => new Section(this, other).Length;
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
