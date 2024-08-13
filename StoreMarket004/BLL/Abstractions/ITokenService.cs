namespace StoreMarket004.BLL.Abstractions
{
    public interface ITokenService
    {
        public string GenerateToken(string email, string roleName);
        public string GetRoleNameFromToken(string stream);
    }
}
