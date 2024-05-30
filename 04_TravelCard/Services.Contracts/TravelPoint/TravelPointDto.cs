
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// ДТО точки остановки.
    /// </summary>
    public class TravelPointDto
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

        /// <summary>
        /// Время остановки
        /// </summary>
        public DateTime WaitingTime { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
