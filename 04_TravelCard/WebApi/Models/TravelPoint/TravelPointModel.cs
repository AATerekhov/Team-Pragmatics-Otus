using System.ComponentModel.DataAnnotations;

namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// Модель точки остановки.
    /// </summary>
    public class TravelPointModel
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

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}
