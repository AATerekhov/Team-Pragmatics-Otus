using UserApi.DataAccess.Entities.Base;

namespace UserApi.DataAccess.Entities
{
    public class User:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TelegramLogin { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
    }
}
