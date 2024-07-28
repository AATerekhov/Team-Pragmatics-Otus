using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Road
    {
        public RoadPoint? Start { get; set; }
        public RoadPoint? Finish { get; set; }
        public double Offset { get; set; }

        public override string ToString()
        {
            return $"{Start}->{Finish}";
        }
    }
}
