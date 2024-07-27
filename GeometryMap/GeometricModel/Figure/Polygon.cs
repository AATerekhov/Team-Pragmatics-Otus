using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GeometryMap.GeometricModel.Figure
{
    public class Polygon: IFigure, IPolySections
    {
		private Section[] _sections;
        private List<Point> _points;
        public Section[] Sections => _sections;
        public List<Point> Points => _points;

        public Polygon(List<Point> points)
        {
            _points = points;
            _sections = new Section[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                if (i == points.Count - 1) _sections[i] = new Section(points[i], points[0]);
                else _sections[i] = new Section(points[i], points[i+1]);
            }
        }

        public bool CheckInsertPoint(Point point) 
        {
            if (!InABCD(point)) return false;

            var distance = GetABCD().GetMainDiaginal().Length;
            Vector vector = new Vector(1,1).Normalize(distance);
            var section = new Section(point, point+ vector);

            int count = 0;
            for (int i = 0; i < Sections.Length; i++)
            {
                if (Sections[i].Intersection(section)) count++;
            }
            if (count == 0) return false;

            if (count % 2 == 0) return false;
            else return true;
        }

        public ABCD GetABCD()
        {
            return new ABCD(this);
        }

        public bool InABCD(Point point)
        {
            return GetABCD().In(point);
        }
    }
}
