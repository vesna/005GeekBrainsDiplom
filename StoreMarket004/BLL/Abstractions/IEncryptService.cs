namespace StoreMarket004.BLL.Abstractions
{
    public interface IEncryptService
    {
        byte[] GenerateSalt();
        byte[] HashPassword(string password, byte[] salt);
    }
}
