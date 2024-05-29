
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Travel_point")]
    public class TravelPoint : IEntity<int>
    {
        [Key, Column("tp_ID")]
        public int Id { get; set; }

        /// <summary>
        /// Точка остановки
        /// </summary>
        [Column("point_map"), MaxLength(90)]
        public string PointMap { get; set; }

        /// <summary>
        /// Описание остановки
        /// </summary>
        [Column("point_desc"), MaxLength(80)]
        public string PointDesc { get; set; }

        /// <summary>
        /// Время остановки
        /// </summary>
        [Column("waiting_time")]
        public DateTime WaitingTime { get; set; }

        [Column("travel_ID")]
        public int TravelId { get; set; }

        public Travel Travel { get; set; }
    }
}
