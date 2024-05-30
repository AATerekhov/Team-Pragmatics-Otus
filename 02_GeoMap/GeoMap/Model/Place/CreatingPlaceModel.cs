namespace GeoMap.Model.Place
{
    public class CreatingPlaceModel
    {
        public int PlaceTypeId { get; set; }
        /// <summary>
        /// Название локации
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public double Latitude { get; set; }
    }
}
