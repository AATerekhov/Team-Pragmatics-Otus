using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel.Figure
{
    public class Polyline : IFigure, IPolySections
    {
        private List<Section> _sections;
        private List<Point> _points;
        public Section[] Sections => _sections.ToArray();
        public List<Point> Points => _points;

        public Polyline(List<Point> points)
        {
            _points = points;
            var sections = new Section[points.Count-1];
            for (int i = 0; i < points.Count-1; i++)
            {
                sections[i] = new Section(points[i], points[i + 1]);
            }
            _sections = sections.ToList();
        }
        public ABCD GetABCD()
        {
            return new ABCD(this);
        }

        public bool InABCD(Point point)
        {
            return GetABCD().In(point);
        }
        public override string ToString()
        {
            var result = string.Empty;
            for (int i = 0; i < Points.Count; i++)
            {
                result += Points[i] + "-";
            }
            return result.TrimEnd('-');
        }


    }
}
