using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Fuelling")]
    public class Fuelling:Location
    {
        /// <summary>
        /// На заправке есть кафе.
        /// </summary>
        public bool IsCafe { get; set; }
        /// <summary>
        /// На заправке есть магазин продуктов.
        /// </summary>
        public bool IsFoodShop { get; set; }
    }
}
