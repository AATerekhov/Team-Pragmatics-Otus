using UserApi.DataAccess.BusinessLogic.Models.Base;

namespace UserApi.DataAccess.BusinessLogic.Models
{
    public class UserModel : IModel<Guid>
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Password { get; init; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
    }
}
