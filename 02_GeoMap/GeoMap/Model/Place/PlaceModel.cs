using GeoMap.Model.PlaceType;

namespace GeoMap.Model.Place
{
    public class PlaceModel:LocationModel
    {
        /// <summary>
        /// Описание места.
        /// </summary>
        public string? Description { get; set; }
        public int PlaceTypeId { get; set; }
        /// <summary>
        /// Тип места посещения.
        /// </summary>
        public PlaceTypeModel? PlaceType { get; set; }
    }
}
