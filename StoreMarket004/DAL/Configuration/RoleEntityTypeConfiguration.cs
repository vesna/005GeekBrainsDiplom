using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreMarket004.DAL.Models;

namespace StoreMarket004.DAL.Configuration
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
           /* builder.HasData(Enum.GetValues(typeof(RoleType)).Cast<RoleType>()
                .Select(e => new Role()
                {
                    Id = (int)e,
                    Name = e.ToString()
                }));*/
            builder.HasKey(x => x.Id);
        }
    }
}
