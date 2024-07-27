using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel.Figure
{
    public class Section : IFigure
    {
        readonly private Point[] _points = new Point[2];
        public Point[] Points => _points;
        public Vector Main => Finish - Start;
        public double Length => Main.GetLength();
        public Point Start
        {
            get { return _points[0]; }
            set { _points[0] = value; }
        }
        public Point Finish
        {
            get { return _points[1]; }
            set { _points[1] = value; }
        }
        public Section(Point start, Point finish)
        {
            _points[0] = start;
            _points[1] = finish;
        }

        public Line GetLine()
        {
            if (this.Main.IsZero) return null;
            else return new Line(this);
        }
        public bool Intersection(Section other, out Point cross)
        {
            cross = new Point();

            var line1 = this.GetLine();
            var line2 = other.GetLine();
            if (line1 == null | line2 == null) return false;
            if (line1.Parallel(line2)) return false;

            if (!line1.Intersection(line2, out cross)) return false;

            if (this.InABCD(cross) && other.InABCD(cross)) return true;
            else return false;
        }
        public bool Intersection(Section other) 
        {
            return this.Intersection(other, out Point point);
        }
        public ABCD GetABCD()
        {
            return new ABCD(this);
        }
        public bool InABCD(Point point)
        {
            return GetABCD().In(point);
        }
        public Polygon Offset(double offset) 
        {
            var vector = Main.Normalize(offset);
            var vectorUp = vector.Rotation(90);
            var vectorBack = vectorUp.Rotation(90);
            var vectorDown = vectorBack.Rotation(90);
            List<Point> points = new List<Point>()
            {
                Start+vectorBack+vectorUp,
                Finish + vector + vectorUp,
                Finish + vector + vectorDown,
                Start+vectorBack+vectorDown,
            };
            return new Polygon(points);
        }
    }
}
