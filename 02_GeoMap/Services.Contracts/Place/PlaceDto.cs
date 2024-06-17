using Services.Contracts.PlaceType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Contracts.Place
{
    public class PlaceDto:LocationDto
    {
        /// <summary>
        /// Описание места.
        /// </summary>
        public string? Description { get; set; }
        public int PlaceTypeId { get; set; }

        /// <summary>
        /// Тип места посещения.
        /// </summary>
        public PlaceTypeDto? PlaceType { get; set; }

    }
}
