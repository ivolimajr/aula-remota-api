using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class EdrivingConfigurations : IEntityTypeConfiguration<EdrivingModel>
    {
        public void Configure(EntityTypeBuilder<EdrivingModel> builder)
        {
            builder.ToTable("Edriving");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.LevelId).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(e => e.UserId).HasColumnType("int").IsRequired().IsRequired();

            builder.HasIndex(e => e.Name);

            builder.HasOne(e => e.Level).WithMany(e => e.Edrivings).HasForeignKey(e => e.LevelId);
            builder.HasOne(e => e.User).WithOne(e => e.Edriving).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.PhonesNumbers).WithOne(e => e.Edriving);
        }
    }
}
