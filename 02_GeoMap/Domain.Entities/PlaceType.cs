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
        [Key, Column("PlaceTypeID")]
        public int Id { get; set; }
        [MaxLength(25)]
        public string? Name { get; set; }
        [MaxLength(40)]
        public string? Description { get; set; }
        public ICollection<Place> Places { get; set; }

    }
}
