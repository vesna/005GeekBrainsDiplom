using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StoreMarket004.DAL.Contexts
{
    public class AuthContextDesignTimeFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AuthContext>();
            options.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Username=postgres;Password=1;Database=AuthDB");
            return new AuthContext(options.Options);
        }
    }
    
}
