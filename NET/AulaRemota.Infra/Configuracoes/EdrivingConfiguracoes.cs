using AulaRemota.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    class EdrivingConfiguracoes : IEntityTypeConfiguration<EdrivingModel>
    {
        public void Configure(EntityTypeBuilder<EdrivingModel> builder)
        {
            builder.ToTable("Edriving");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.CargoId).HasColumnType("int").IsRequired().IsRequired();

            builder.HasIndex(e => e.Nome);

            builder.HasOne(e => e.Cargo);
            builder.HasOne(e => e.Usuario);
            builder.HasMany(e => e.Telefones);
        }
    }
}
