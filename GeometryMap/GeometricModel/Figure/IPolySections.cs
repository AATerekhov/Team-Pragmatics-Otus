using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryMap.GeometricModel.Figure
{
    public interface IPolySections
    {
        public Section[] Sections { get; }
        public List<Point> Points { get; }
    }
}
