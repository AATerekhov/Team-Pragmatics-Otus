namespace Services.Contracts.User
{
    /// <summary>
    /// ДТО создания юзера
    /// </summary>
    public class CreatingUserDto
    {
        /// <summary>
        /// Идентификатор юзера.
        /// </summary>
        public int Id { get; set; }

        //public int TravelId { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
