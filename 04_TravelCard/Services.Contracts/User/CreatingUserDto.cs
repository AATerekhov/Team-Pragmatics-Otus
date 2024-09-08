namespace Services.Contracts.User
{
    /// <summary>
    /// ДТО создания юзера
    /// </summary>
    public class CreatingUserDto
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
