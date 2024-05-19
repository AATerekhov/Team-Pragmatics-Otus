using GeoMap.Model.Place;

namespace GeoMap.Model.Fuelling
{
    public class FuellingModel:LocationModel
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
