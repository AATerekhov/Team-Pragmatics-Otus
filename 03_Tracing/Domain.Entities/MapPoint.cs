using System;

namespace Domain.Entities
{
    /// <summary>
    /// Географические координаты.
    /// </summary>
    public class MapPoint:IEntity<int>
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
