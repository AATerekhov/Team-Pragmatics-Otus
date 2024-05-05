using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IEntity<int>
    {
        [Key,Column("UserID")]
        public int Id { get; set; }
        [MaxLength(25)]
        [Required()]
        public string? Logo { get; set; }
        [MaxLength(40)]
        public string? Name { get; set; }
        [MaxLength(40)]
        public string? Surname { get; set; }
        [MaxLength(40)]
        public string? Email { get; set; }
        
    }
}
