using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("PlaceType")]
    public class PlaceType : IEntity<int>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key, Column("PlaceTypeID")]
        public int Id { get; set; }
        /// <summary>
        /// Название типа.
        /// </summary>
        [MaxLength(25)]
        public required string Name { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        [MaxLength(40)]
        public string? Description { get; set; }
        /// <summary>
        /// Список мест реализующий данный тип.
        /// </summary>
        public ICollection<Place>? Places { get; set; }

    }
}
