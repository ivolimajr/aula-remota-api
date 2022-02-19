using AulaRemota.Infra.Entity.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class ApiUserConfigurations : IEntityTypeConfiguration<ApiUserModel>
    {
        public void Configure(EntityTypeBuilder<ApiUserModel> builder)
        {
            builder.ToTable("ApiUser");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.UserName).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Password).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.RefreshToken).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");
            builder.HasMany(e => e.Roles);
        }
    }
}
