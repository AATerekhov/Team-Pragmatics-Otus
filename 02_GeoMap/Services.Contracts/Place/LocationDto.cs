using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Place
{
    public abstract class LocationDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название локации
        /// </summary>
        public string? Name { get; set; }
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
