using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.GeometricModel
{
    public class CoordinateSystem
    {
        public CoordinateSystem(Vector x, Vector y, Point center)
        {
            X = x;
            Y = y;
            Center = center;
        }
        public Vector X { get; set; }
        public Vector Y { get; set; }
        public Point Center { get; set; }
    }
}
