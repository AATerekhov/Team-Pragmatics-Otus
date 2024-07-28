using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoadPoint
    {
        /// <summary>
        /// Долгота 
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        public override string ToString()
        {
            return $"@{Longitude},{Latitude}";
        }
    }
}
