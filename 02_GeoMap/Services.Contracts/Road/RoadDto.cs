using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Road
{
    public class RoadDto
    {
        public ICollection<RoadPointDto>? RoadPoints { get; set; }
        public double Offset { get; set; }
    }
}
