using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.DataAccess.BusinessLogic.Models
{
    public class CreateUserModel
    {
        public string? Name { get; init; }
        public string? Password { get; init; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
    }
}
