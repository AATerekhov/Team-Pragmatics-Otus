using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel
{
    public struct Vector
    {
        public double X;
        public double Y;
        public bool IsZero { get { if (X == 0 && Y == 0) return true; else return false; } }
    
        public Vector(double x, double y)
        {
            X = Math.Round(x, 6);
            Y = Math.Round(y, 6);
        }
        public Vector Normalize() 
        {
            var length = this.GetLength();
            return new Vector(X / length, Y / length);
        }
        public Vector Normalize(double value)
        {
            var length = this.GetLength();
            return new Vector(value*X / length, value*Y / length);
        }
        public Vector Rotation(double angle)
        {
            X = X * Math.Cos(angle) - Y * Math.Sin(angle);
            Y = X * Math.Sin(angle) + Y * Math.Cos(angle);
            return new Vector(X, Y);
        }

        public double GetLength() 
        {
            return Math.Sqrt(X * X + Y * Y);            
        }
 
        public static Vector operator +(Vector a, Vector b) 
        {
            return new Vector(a.X+b.X, a.Y+b.Y);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.X*b, a.Y*b);
        }
    }
}
