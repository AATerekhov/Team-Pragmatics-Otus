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
        [Key,Column("LocationID")]
        public Guid Id { get; set; }

        [MaxLength(40)]
        public string? Name { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
