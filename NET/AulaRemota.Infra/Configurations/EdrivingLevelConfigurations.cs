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
            builder.ToTable("edrivingLevel");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Level).HasColumnType("varchar").HasMaxLength(70).IsRequired();

            builder.HasIndex(e => e.Level);

            builder.HasMany(e => e.Edrivings).WithOne(e => e.Level).HasForeignKey(e => e.LevelId);

        }
    }
}
