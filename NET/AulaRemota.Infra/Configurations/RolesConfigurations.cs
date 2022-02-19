using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class RolesConfigurations : IEntityTypeConfiguration<RolesModel>
    {
        public void Configure(EntityTypeBuilder<RolesModel> builder)
        {
            builder.ToTable("roles");

            builder.HasIndex(e => e.Role);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Role).HasColumnType("varchar").HasMaxLength(20).IsRequired();

            builder.HasMany(e => e.ApiUsers);
            builder.HasMany(e => e.Usuarios);
        }
    }
}
