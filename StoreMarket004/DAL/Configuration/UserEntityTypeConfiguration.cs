using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreMarket004.DAL.Models;

namespace StoreMarket004.DAL.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Salt).IsRequired();

            builder.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId);
        }
    }
}
