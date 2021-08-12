using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes.Auto_Escola
{
    public class InstrutorConfiguracoes : IEntityTypeConfiguration<InstrutorModel>
    {
        public void Configure(EntityTypeBuilder<InstrutorModel> builder)
        {
            builder.ToTable("Instrutor");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Identidade).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Orgão).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Aniversario).HasColumnType("datetime");
            builder.Property(e => e.EnderecoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UsuarioId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.Usuario).WithOne(e => e.Instrutor);
            builder.HasOne(e => e.Endereco).WithOne(e => e.Instrutor);
            builder.HasMany(e => e.AutoEscolas).WithMany(e => e.Instrutores);
        }
    }
}