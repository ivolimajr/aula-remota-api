using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<AddressModel>
    {
        public void Configure(EntityTypeBuilder<AddressModel> builder)
        {
            builder.ToTable("address");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Uf).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.Cep).HasColumnType("varchar").HasMaxLength(12);
            builder.Property(e => e.Address).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.District).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.City).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Number).HasColumnType("varchar").HasMaxLength(50);

            builder.HasOne(e => e.Instructor).WithOne(e => e.Address);
            builder.HasOne(e => e.Partnner).WithOne(e => e.Address);
            builder.HasOne(e => e.Administrative).WithOne(e => e.Address);
            builder.HasOne(e => e.DrivingSchool).WithOne(e => e.Address);
            builder.HasMany(e => e.Students).WithOne(e => e.Address).HasForeignKey(e => e.AddressId);
        }
    }
}
