namespace GeoMap.Model.Place
{
    public class LocationModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название локации
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }
    }
}
