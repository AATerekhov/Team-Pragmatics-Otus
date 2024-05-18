using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.PlaceType
{
    public class CreatingPlaceTypeDto
    {
        /// <summary>
        /// Название типа.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; set; }
    }
}
