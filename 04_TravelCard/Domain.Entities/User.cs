using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Column("user_ID")]
        public Guid Id { get; set; }
        
        [Column("travel_ID")]
        public int? TravelId { get; set; }
        
        public ICollection<Travel>? Travels { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        [Column("deleted")]
        public bool Deleted { get; set; }

        public string? Name { get; init; }

        public DateTime DateRegistration { get; set; }

        public string? Email { get; set; }
    }
}
