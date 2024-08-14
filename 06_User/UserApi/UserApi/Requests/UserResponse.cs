namespace UserApi.Requests
{
    public class UserResponse
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Password { get; init; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
    }
}
