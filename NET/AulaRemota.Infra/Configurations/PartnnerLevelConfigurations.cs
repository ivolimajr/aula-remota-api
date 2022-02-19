using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class PartnnerLevelConfigurations : IEntityTypeConfiguration<PartnnerLevelModel>
    {
        public void Configure(EntityTypeBuilder<PartnnerLevelModel> builder)
        {
            builder.ToTable("ParceiroCargo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Cargo).HasColumnType("varchar").HasMaxLength(70).IsRequired();

            builder.HasMany(e => e.Parceiros).WithOne(e => e.Cargo).HasForeignKey(e => e.CargoId);

            builder.HasIndex(e => e.Cargo);

        }
    }
}
