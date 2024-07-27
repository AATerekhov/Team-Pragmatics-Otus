using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel.Figure
{
    public interface IFigure
    {
        bool InABCD(Point point);
        ABCD GetABCD();
    }
}
