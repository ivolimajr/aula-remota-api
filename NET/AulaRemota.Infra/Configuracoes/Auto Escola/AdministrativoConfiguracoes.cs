using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes.Auto_Escola
{
    public class AdministrativoConfiguracoes : IEntityTypeConfiguration<AdministrativoModel>
    {
        public void Configure(EntityTypeBuilder<AdministrativoModel> builder)
        {
            builder.ToTable("Administrativo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Identidade).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Orgão).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Aniversario).HasColumnType("datetime");
            builder.Property(e => e.EnderecoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UsuarioId).HasColumnType("int").IsRequired();
            builder.Property(e => e.AutoEscolaId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.Usuario).WithOne(e => e.Administrativo);
            builder.HasOne(e => e.Endereco).WithOne(e => e.Administrativo);
            builder.HasOne(e => e.AutoEscola).WithMany(e => e.Administrativos).HasForeignKey(e => e.AutoEscolaId);
        }
    }
}