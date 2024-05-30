using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Место для посещения.
    /// </summary>
    [Table("Place")]
    public class Place:Location
    {
        /// <summary>
        /// Описание места.
        /// </summary>
        [MaxLength(40)]
        public string? Description { get; set; }
        /// <summary>
        /// Внешний ключ типа места.
        /// </summary>
        public  int PlaceTypeID { get; set; }
        /// <summary>
        /// Тип места посещения.
        /// </summary>
        public PlaceType? PlaceType { get; set; }
    }
}
