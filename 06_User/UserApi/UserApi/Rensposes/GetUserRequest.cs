namespace UserApi.Rensposes
{
    public class GetUserRequest
    {
        public required string Name { get; init; }
        public string? Password { get; init; }
    }
}
