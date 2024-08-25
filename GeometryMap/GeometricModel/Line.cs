using GeometryMap.GeometricModel.Figure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel
{
    public class Line
    {
        readonly private double[] _options = new double[3];
        public double A { get { return _options[0]; } }
        public double B { get { return _options[1]; } }
        public double C { get { return _options[2]; } }
        public Line(double a, double b, double c)
        {
            _options[0] = a; _options[1] = b; _options[2] = c;
        }
        public Line(Section s)
        {
            if (s.Finish.X - s.Start.X == 0)//Вертикаль
            {
                _options[0] = 1;
                _options[1] = 0;
                _options[2] = -1 * s.Start.X;
            }
            else if (s.Finish.Y - s.Start.Y == 0) //Горизонталь
            {
                _options[0] = 0;
                _options[1] = 1;
                _options[2] = -1 * s.Start.Y;
            }
            else
            {
                var m = (s.Finish.Y - s.Start.Y) / (s.Finish.X - s.Start.X);//Формула наклона.
                _options[0] = m;
                _options[1] = -1;
                _options[2] = s.Start.Y - m * s.Start.X;
            }            
        }
        public bool Parallel(Line other) 
        {
            if (this.A == other.A) return true;
            else return false;
        }
        public bool Intersection(Line other, out Point point) 
        {
            point = new Point();
            if (this.Parallel(other)) return false;

            var x = (this.B * other.C - other.B * this.C) / (this.A * other.B - other.A * this.B);
            var y = (this.C * other.A - other.C * this.A) / (this.A * other.B - other.A * this.B);
            point = new Point(x,y);
            return true;
        }
        public override string ToString()
        {
            return $"{A}x+{B}y+{C}=0";
        }
    }
}
