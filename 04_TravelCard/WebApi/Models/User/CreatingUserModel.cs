namespace Services.Contracts.User
{
    /// <summary>
    /// Модель создаваемого юзера
    /// </summary>
    public class CreatingUserModel
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
