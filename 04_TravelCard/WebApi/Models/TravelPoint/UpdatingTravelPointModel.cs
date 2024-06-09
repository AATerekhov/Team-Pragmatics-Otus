namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// Модель редактирования точки остановки
    /// </summary>
    public class UpdatingTravelPointModel
    {
        /// <summary>
        /// Точка остановки
        /// </summary>
        public string PointMap { get; set; }

        /// <summary>
        /// Описание остановки
        /// </summary>
        public string PointDesc { get; set; }

        /// <summary>
        /// Время остановки
        /// </summary>
        public double WaitingTimeCountMinutes { get; set; }
    }
}
