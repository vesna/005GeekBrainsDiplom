using Contracts.Requests;
using Contracts.Responses;

namespace StoreMarket004.BLL.Abstractions
{
    public interface IAuthService
    {
        public string? Login(string email, string password);
        public string? Register(UserAuthRequest request);
    }
}
