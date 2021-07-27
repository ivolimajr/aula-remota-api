using AulaRemota.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    class ParceiroConfiguracoes : IEntityTypeConfiguration<ParceiroModel>
    {
        public void Configure(EntityTypeBuilder<ParceiroModel> builder)
        {
            builder.ToTable("Parceiro");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Cnpj).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.CargoId).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(e => e.EnderecoId).HasColumnType("int").IsRequired().IsRequired();
            builder.Property(e => e.UsuarioId).HasColumnType("int").IsRequired().IsRequired();

            builder.HasIndex(e => e.Nome);

            builder.HasOne(e => e.Cargo);
            builder.HasOne(e => e.Usuario);
            builder.HasMany(e => e.Telefones);
        }
    }
}
