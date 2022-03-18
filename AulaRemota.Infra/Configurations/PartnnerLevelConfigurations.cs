using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class PartnnerLevelConfigurations : IEntityTypeConfiguration<PartnnerLevelModel>
    {
        public void Configure(EntityTypeBuilder<PartnnerLevelModel> builder)
        {
            builder.ToTable("partnnerLevel");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Level).HasColumnType("varchar").HasMaxLength(70).IsRequired();

            builder.HasMany(e => e.Partnners).WithOne(e => e.Level).HasForeignKey(e => e.LevelId);

            builder.HasIndex(e => e.Level);

        }
    }
}
