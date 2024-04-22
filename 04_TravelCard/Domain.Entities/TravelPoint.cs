
namespace Domain.Entities
{
    internal class TravelPoint
    {
        /// <summary>
        /// Точка остановки
        /// </summary>
        public object? PointMap { get; set; }

        /// <summary>
        /// Описание остановки
        /// </summary>
        public string PointDesc { get; set; }

        /// <summary>
        /// Время остановки
        /// </summary>
        public DateTime? WaitingTime { get; set; }
    }
}
