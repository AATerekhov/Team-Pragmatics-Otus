using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("User")]
    public class User:IEntity<Guid>
    {
        [Key, Column("UserID")]
        public Guid Id { get; set; }
        [MaxLength(25)]
        [Required()]
        public string? Logo { get; set; }
        [MaxLength(40)]
        public string? Name { get; set; }
        [MaxLength(40)]
        public string? Surname { get; set; }
        [MaxLength(40)]
        [Required()]
        public string? Email { get; set; }

    }
}
