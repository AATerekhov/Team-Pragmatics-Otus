namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// ДТО редактирования точки остановки
    /// </summary>
    public class UpdatingTravelPointDto
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
        public DateTime WaitingTime { get; set; }
    }
}
