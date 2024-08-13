namespace Contracts.Responses
{
    public class UserAuthRespose
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string RoleName { get; set; }
    }
}
