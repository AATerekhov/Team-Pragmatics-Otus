using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.GeometricModel
{
    public struct Vector
    {
        public double X;
        public double Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Normalize() 
        {
            var length = this.GetLength();
            X = X / length;
            Y = Y / length;
        }
        public void Normalize(double value)
        {
            var length = this.GetLength();
            X = value * X / length;
            Y = value * Y / length;
        }
        public void Rotation(double angle)
        {
            X = X * Math.Cos(angle) - Y * Math.Sin(angle);
            Y = X * Math.Sin(angle) + Y * Math.Cos(angle);
        }

        public double GetLength() 
        {
            return Math.Sqrt(X * X + Y * Y);            
        }

        public static Vector GetVector(RoadPoint roadPoint) { return new Vector(roadPoint.Longitude, roadPoint.Latitude); }       
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
