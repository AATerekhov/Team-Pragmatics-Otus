
namespace GeoMap.Model.User
{
    public class UserModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя учетной записи.
        /// </summary>
        public string? Logo { get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Адресс электронной почты.
        /// </summary>
        public string? Email { get; set; }
    }
}
