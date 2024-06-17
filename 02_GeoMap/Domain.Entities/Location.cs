using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Location : IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key,Column("LocationID")]
        public Guid Id { get; set; }
        /// <summary>
        /// Название локации
        /// </summary>
        [MaxLength(40)]
        public string? Name { get; set; }
        /// <summary>
        /// Долгота 
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
