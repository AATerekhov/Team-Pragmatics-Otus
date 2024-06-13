using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IEntity<int>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key, Column("user_ID")]
        public int Id { get; set; }
        
        [Column("travel_ID")]
        public int? TravelId { get; set; }
        
        public ICollection<Travel>? Travels { get; set; }

        [Column("login")]
        public string Login {  get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
