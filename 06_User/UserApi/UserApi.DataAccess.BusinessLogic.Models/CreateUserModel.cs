using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.DataAccess.BusinessLogic.Models
{
    public class CreateUser
    {
        public string? Name { get; init; }
        public string? Password { get; init; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TelegramLogin { get; set; }
        public DateOnly BirthDate { get; init; }
        public string? Gender { get; set; }
    }
}
