using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Place")]
    public class Place:Location
    {
        [MaxLength(40)]
        public string? Description { get; set; }
        public  int PlaceTypeID { get; set; }
        public PlaceType? PlaceType { get; set; }
    }
}
