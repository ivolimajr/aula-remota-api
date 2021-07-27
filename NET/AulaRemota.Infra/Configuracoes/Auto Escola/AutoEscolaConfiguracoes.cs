using AulaRemota.Core.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes.Auto_Escola
{
    public class AutoEscolaConfiguracoes : IEntityTypeConfiguration<AutoEscolaModel>
    {
        public void Configure(EntityTypeBuilder<AutoEscolaModel> builder)
        {
            builder.ToTable("AutoEscola");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.RazaoSocial).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.NomeFantasia).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.InscricaoEstadual).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Cnpj).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Site).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(e => e.DataFundacao).HasColumnType("datetime");
            builder.Property(e => e.EnderecoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UsuarioId).HasColumnType("int").IsRequired();
            builder.Property(e => e.CargoId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.Usuario).WithOne(e => e.AutoEscola);
            builder.HasOne(e => e.Endereco).WithOne(e => e.AutoEscola);
            builder.HasOne(e => e.Cargo).WithMany(e => e.AutoEscolas).HasForeignKey(e => e.CargoId);
            builder.HasMany(e => e.Administrativos).WithOne(e => e.AutoEscola).HasForeignKey(e => e.AutoEscolaId);
            builder.HasMany(e => e.Instrutores).WithMany(e => e.AutoEscolas);
            builder.HasMany(e => e.Turmas).WithOne(e => e.AutoEscola).HasForeignKey(e => e.AutoEscolaId);
            builder.HasMany(e => e.Arquivos).WithOne(e => e.AutoEscola);
            builder.HasMany(e => e.Telefones).WithOne(e => e.AutoEscola);
        }
    }
}