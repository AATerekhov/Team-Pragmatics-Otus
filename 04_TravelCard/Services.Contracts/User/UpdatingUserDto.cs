namespace Services.Contracts.User
{
    /// <summary>
    /// ДТО редактирования юзера
    /// </summary>
    public class UpdatingUserDto
    {
        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

        public string? Name { get; init; }

        public DateTime DateRegistration { get; set; }

        public string? Email { get; set; }
    }
}
