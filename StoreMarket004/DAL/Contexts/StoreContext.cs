using Microsoft.EntityFrameworkCore;
using StoreMarket004.DAL.Models;

namespace StoreMarket004.DAL.Contexts
{
    public class StoreContext : DbContext
    {
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductStore> ProductStores { get; set; }
        private string _connectionString;

        public StoreContext() { }

        public StoreContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StoreContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder./*LogTo(Console.WriteLine).*/UseLazyLoadingProxies().UseNpgsql("Host=localhost;Username=postgres;Password=1;Database=StoreDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("product_pkey");
                entity.ToTable("Product");
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(x => x.Description).HasMaxLength(250).HasColumnName("description");
                entity.HasMany(s => s.Store).WithMany(s => s.Product).UsingEntity<ProductStore>();


                /*  .UsingEntity(
                      "ProductStore",
                      l => l.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
                      r => r.HasOne(typeof(Store)).WithMany().HasForeignKey("StoreId").HasPrincipalKey(nameof(Store.Id)),
                      j => j.HasKey("ProductId", "StoreId"));*/
                entity.HasOne(c => c.Category).WithMany(p => p.Product).HasForeignKey(c => c.CategoryId).HasConstraintName("product_from_category_id_fkey");

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("category_pkey");
                entity.ToTable("Category");
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(x => x.Description).HasMaxLength(250).HasColumnName("description");

            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("store_pkey");
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
                entity.HasIndex(x => x.Name).IsUnique();
                entity.ToTable("Store");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
