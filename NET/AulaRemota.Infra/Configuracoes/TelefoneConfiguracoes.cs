using AulaRemota.Core.Entity;
using AulaRemota.Core.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    class TelefoneConfiguracoes : IEntityTypeConfiguration<TelefoneModel>
    {
        public void Configure(EntityTypeBuilder<TelefoneModel> builder)
        {
            builder.ToTable("telefone");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Telefone).HasColumnType("varchar").HasMaxLength(20).IsRequired();

        }
    }
}
