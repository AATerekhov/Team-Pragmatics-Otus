using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.Place;

namespace Services.Contracts.PlaceType
{
    public class PlaceTypeDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
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
