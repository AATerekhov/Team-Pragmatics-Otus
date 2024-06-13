﻿
namespace Services.Contracts.User
{
    /// <summary>
    /// ДТО юзера.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        //public int TravelId { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
