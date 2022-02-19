using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class EdrivingLevelConfigurations : IEntityTypeConfiguration<EdrivingLevelModel>
    {
        public void Configure(EntityTypeBuilder<EdrivingLevelModel> builder)
        {
            builder.ToTable("EdrivingCargo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Cargo).HasColumnType("varchar").HasMaxLength(70).IsRequired();

            builder.HasIndex(e => e.Cargo);

            builder.HasMany(e => e.Edrivings).WithOne(e => e.Cargo).HasForeignKey(e => e.CargoId);

        }
    }
}
