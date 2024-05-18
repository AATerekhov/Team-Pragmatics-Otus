using Services.Contracts.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Fuelling
{
    public class FuellingDto:LocationDto
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
