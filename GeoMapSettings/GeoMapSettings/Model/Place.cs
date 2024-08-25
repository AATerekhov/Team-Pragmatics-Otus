using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings.Model
{
    public class Place
    { 
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Description { get; set; }
        public int PlaceTypeID { get; set; }
        public override string ToString()
        {
            return "[" + Name + " | " + Description + "]";
        }
    }
}