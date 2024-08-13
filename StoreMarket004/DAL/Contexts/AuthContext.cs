using Microsoft.EntityFrameworkCore;
using StoreMarket004.DAL.Models;
using System.Reflection;

namespace StoreMarket004.DAL.Contexts
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
