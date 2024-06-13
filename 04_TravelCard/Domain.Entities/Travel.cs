using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Travel : IEntity<int>
    {
        [Key, Column("travel_ID")]
        public int Id { get; set; }

        /// <summary>
        /// Описание путешествия
        /// </summary>
        [Column("description"), MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Менеджер путешествия
        /// </summary>
        //public string ManagerID { get; set; }

        /// <summary>
        /// Список компаньонов
        /// </summary>
        public ICollection<User>? Users { get; set; }

        /// <summary>
        /// Список остановок
        /// </summary>
        public ICollection<TravelPoint>? TravelPoints { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        [Column("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Координата начала путешествия
        /// </summary>
        [Column("start_point")]
        public string StartPoint { get; set; }

        /// <summary>
        /// Координата конца путешествия
        /// </summary>
        [Column("finish_point")]
        public string FinishPoint { get; set; }

    }
}
