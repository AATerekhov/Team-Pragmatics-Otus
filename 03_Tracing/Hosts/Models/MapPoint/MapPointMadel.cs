namespace TracingProgmatica.Models.MapPoint
{
    public class MapPointMadel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }
    }
}
