using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class PartnnerConfigurations : IEntityTypeConfiguration<PartnnerModel>
    {
        public void Configure(EntityTypeBuilder<PartnnerModel> builder)
        {
            builder.ToTable("partnner");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Cnpj).HasColumnType("varchar").HasMaxLength(14).IsRequired();

            builder.Property(e => e.LevelId).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(e => e.AddressId).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(e => e.UserId).HasColumnType("int").IsRequired().IsRequired();


            builder.HasOne(e => e.Level).WithMany(e => e.Partnners).HasForeignKey(e => e.LevelId);
            builder.HasOne(e => e.User).WithOne(e => e.Partnner);
            builder.HasMany(e => e.PhonesNumbers).WithOne(e => e.Partnner);

            builder.HasIndex(e => e.Name);
        }
    }
}
