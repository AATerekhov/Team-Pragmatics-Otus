
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Создатель карточки
    /// </summary>
    public class Manager : IUser
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key, Column("user_ID")]
        public int Id { get; set; }

        [Column("travel_ID")]
        public int TravelId { get; set; }

        public Travel Travel { get; set; }
    }
}
