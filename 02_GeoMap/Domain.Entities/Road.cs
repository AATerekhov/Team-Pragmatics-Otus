using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Road
    {
        public ICollection<RoadPoint>? RoadPoints { get; set; }
        public double Offset { get; set; }

        public override string ToString()
        {
            var result = string.Empty;
            var points = RoadPoints?.ToArray();
            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    result += points[i];
                    if (i != points.Length - 1) result += "->";
                }
            }           
            return result;
        }
    }
}
