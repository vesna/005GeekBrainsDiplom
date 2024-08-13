using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StoreMarket004.Securities
{
    public class JwtConfiguration
    {
        public required string Key { get; init; }
        public required string Issuer {  get; init; }
        public required string Audience { get; init;}

        internal SymmetricSecurityKey GetSingingKey() => new(Encoding.UTF8.GetBytes(Key));
    }
}
