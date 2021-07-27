using AulaRemota.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    public class EnderecoConfiguracoes : IEntityTypeConfiguration<EnderecoModel>
    {
        public void Configure(EntityTypeBuilder<EnderecoModel> builder)
        {
            builder.ToTable("endereco");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Uf).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.Cep).HasColumnType("varchar").HasMaxLength(12);
            builder.Property(e => e.EnderecoLogradouro).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Bairro).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Cidade).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Numero).HasColumnType("varchar").HasMaxLength(50);

            builder.HasOne(e => e.Instrutor).WithOne(e => e.Endereco);
            builder.HasOne(e => e.Parceiro).WithOne(e => e.Endereco);
            builder.HasOne(e => e.Administrativo).WithOne(e => e.Endereco);
            builder.HasOne(e => e.AutoEscola).WithOne(e => e.Endereco);
            builder.HasMany(e => e.Alunos).WithOne(e => e.Endereco).HasForeignKey(e => e.EnderecoId);
        }
    }
}
