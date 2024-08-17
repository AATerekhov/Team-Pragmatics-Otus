namespace UserApi.Rensposes
{
    public class CreateUserRequest
    {
        public required string Name { get; init; }
        public string? Password { get; init; }
        public DateTime DateRegistration { get; set; }
        public string? Email { get; set; }
    }
}
