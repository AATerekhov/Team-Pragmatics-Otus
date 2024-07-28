using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel.Figure
{
    public class ABCD
    {
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        public Point GetMinPoint()=> new Point(MinX, MinY);
        public Point GetMaxPoint() => new Point(MaxX, MaxY);
        public Section GetMainDiaginal() => new Section(GetMinPoint(), GetMaxPoint());
        public ABCD(Section section) 
        {
            var list = section.Points.ToList();
            MinX = list.Select(p => p.X).Min();
            MinY = list.Select(p => p.Y).Min();
            MaxX = list.Select(p => p.X).Max();
            MaxY = list.Select(p => p.Y).Max();
        }
        public ABCD(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }
        public ABCD(IPolySections poly) : this(poly.Sections[0])
        {
            for (int i = 2; i < poly.Points.Count; i++)
            {
                Add(poly.Points[i]);               
            }
        }

        public void Add(Point point)
        {
            if (point.X < MinX) MinX = point.X;
            if (point.Y < MinY) MinY = point.Y;
            if (point.X > MaxX) MaxX = point.X;
            if (point.Y > MaxY) MaxY = point.Y;
        }
        public bool In(Point point)
        {
            if (point.X <= MaxX && point.X >= MinX && point.Y <= MaxY && point.Y >= MinY) return true;
            return false;
        }

        public override string ToString()
        {
            return GetMainDiaginal().ToString();
        }
    }
}
