using System.ComponentModel.DataAnnotations;

namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// ДТО создания остановки
    /// </summary>
    public class CreatingTravelPointDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Точка остановки
        /// </summary>
        public string PointMap { get; set; }

        /// <summary>
        /// Описание остановки
        /// </summary>
        public string PointDesc { get; set; }

        public int TravelId { get; set; }

        /// <summary>
        /// Время остановки
        /// </summary>
        public double WaitingTimeCountMinutes { get; set; }

    }
}
