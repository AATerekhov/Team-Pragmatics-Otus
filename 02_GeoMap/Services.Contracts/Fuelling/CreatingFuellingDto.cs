using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Fuelling
{
    public class CreatingFuellingDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public double Latitude { get; set; }
    }
}
