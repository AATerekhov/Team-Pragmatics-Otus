namespace Services.Contracts.User
{
    /// <summary>
    /// Модель создаваемого юзера
    /// </summary>
    public class CreatingUserModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

        public string? Name { get; init; }

        public DateTime DateRegistration { get; set; }

        public string? Email { get; set; }
    }
}
