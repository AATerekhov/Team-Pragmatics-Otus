using UserApi.DataAccess.Entities.Base;

namespace UserApi.DataAccess.Entities
{
    public class User:IEntity<Guid>
    {
        public Guid Id { get; init; }        
        public string? Name { get; init; }
        public string? Password { get; init; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
    }
}
