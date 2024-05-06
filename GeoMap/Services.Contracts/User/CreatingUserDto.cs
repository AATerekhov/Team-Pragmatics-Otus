using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.User
{
    public class CreatingUserDto
    {
        /// <summary>
        /// Имя учетной записи.
        /// </summary>
        public string? Logo { get; set; }
        /// <summary>
        /// Адресс электронной почты.
        /// </summary>
        public string? Email { get; set; }
    }
}
