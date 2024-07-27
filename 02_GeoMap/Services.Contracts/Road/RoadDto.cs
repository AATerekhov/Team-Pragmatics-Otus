using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Road
{
    public class RoadDto
    {
        public RoadPointDto? Start { get; set; }
        public RoadPointDto? Finish { get; set; }
        public double Offset { get; set; }
    }
}
