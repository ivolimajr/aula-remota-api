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
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.CargoId).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(e => e.UsuarioId).HasColumnType("int").IsRequired().IsRequired();

            builder.HasIndex(e => e.Nome);

            builder.HasOne(e => e.Cargo).WithMany(e => e.Edrivings).HasForeignKey(e => e.CargoId);
            builder.HasOne(e => e.Usuario);
            builder.HasMany(e => e.Telefones).WithOne(e => e.Edriving);
        }
    }
}
