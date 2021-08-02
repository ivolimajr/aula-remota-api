using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    public class TelefoneConfiguracoes : IEntityTypeConfiguration<TelefoneModel>
    {
        public void Configure(EntityTypeBuilder<TelefoneModel> builder)
        {
            builder.ToTable("telefone");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Telefone).HasColumnType("varchar").HasMaxLength(20).IsRequired();

            builder.HasOne(e => e.Administrativo).WithMany(e => e.Telefones);
            builder.HasOne(e => e.Parceiro).WithMany(e => e.Telefones);
            builder.HasOne(e => e.Instrutor).WithMany(e => e.Telefones);
            builder.HasOne(e => e.Edriving).WithMany(e => e.Telefones);
            builder.HasOne(e => e.AutoEscola).WithMany(e => e.Telefones);
            builder.HasOne(e => e.Aluno).WithMany(e => e.Telefones);

        }
    }
}
