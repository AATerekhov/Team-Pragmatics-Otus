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
        [Column("travel_desc"), MaxLength(100)]
        public string TravelDesc { get; set; }

        /// <summary>
        /// Менеджер путешествия
        /// </summary>
        public Manager Manager { get; set; }

        /// <summary>
        /// Список компаньонов
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Список остановок
        /// </summary>
        public ICollection<TravelPoint> TravelPoints { get; set; }

    }
}
