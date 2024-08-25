using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.DataAccess.Entities;

namespace UserApi.DataAccess.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Password).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Email).HasMaxLength(128);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
