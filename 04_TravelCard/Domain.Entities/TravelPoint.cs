
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
        [Column("point_map")]
        public string PointMap { get; set; }

        /// <summary>
        /// Описание остановки
        /// </summary>
        [Column("point_desc")]
        public string PointDesc { get; set; }

        /// <summary>
        /// Время остановки (количество минут)
        /// </summary>
        [Column("waiting_time")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public double WaitingTimeCountMinutes { get; set; }

        [Column("travel_ID")]
        public int TravelId { get; set; }

        public Travel Travel { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
