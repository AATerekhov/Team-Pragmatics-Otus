using Services.Contracts.PlaceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Place
{
    public class UpdatingPlaceDto
    {
        /// <summary>
        /// Название локации
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Описание места.
        /// </summary>
        public string? Description { get; set; }
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
