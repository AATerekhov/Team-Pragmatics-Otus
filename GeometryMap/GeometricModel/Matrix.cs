using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel
{
    public class Matrix
    {
        readonly private double[,] _values = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        public double[,] Values { get { return _values; } }
        public double XfactorOne 
        {
            get { return _values[0, 0]; }
            set {  _values[0, 0] = value; }
        }
        public double XfactorTwo
        {
            get { return _values[0, 1]; }
            set { _values[0, 1] = value; }
        }
        public double XMoving
        {
            get { return _values[0, 2]; }
            set { _values[0, 2] = value; }
        }
        public double YfactorOne
        {
            get { return _values[1, 0]; }
            set { _values[1, 0] = value; }
        }
        public double YfactorTwo
        {
            get { return _values[1, 1]; }
            set { _values[1, 1] = value; }
        }
        public double YMoving
        {
            get { return _values[1, 2]; }
            set { _values[1, 2] = value; }
        }
        public double ZfactorOne
        {
            get { return _values[2, 0]; }
            set { _values[2, 0] = value; }
        }
        public double ZfactorTwo
        {
            get { return _values[2, 1]; }
            set { _values[2, 1] = value; }
        }
        public double ZMoving
        {
            get { return _values[2, 2]; }
            set { _values[2, 2] = value; }
        }
        public Matrix()
        {

        }

        public Matrix(double[,] values)
        {
            this._values = values;
        }
        public override string ToString()
        {
            return $"|{XfactorOne}, {YfactorOne}, {ZfactorOne}| \r\n" +
                   $"|{XfactorTwo}, {YfactorTwo}, {ZfactorTwo}| \r\n" +
                   $"|{XMoving}, {YMoving}, {ZMoving}|";
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            double[,] C = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            double[,] A = a.Values;
            double[,] B = b.Values;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    for (int k = 0; k < B.GetLength(0); k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return new Matrix(C);
        }

    }
}
