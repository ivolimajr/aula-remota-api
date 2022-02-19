using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class DrivingSchoolConfigurations : IEntityTypeConfiguration<DrivingSchoolModel>
    {
        public void Configure(EntityTypeBuilder<DrivingSchoolModel> builder)
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

            //RELACIONAMENTOS
            builder.HasOne(e => e.Usuario).WithOne(e => e.AutoEscola);
            builder.HasOne(e => e.Endereco).WithOne(e => e.AutoEscola);
            builder.HasMany(e => e.Administrativos).WithOne(e => e.AutoEscola).HasForeignKey(e => e.AutoEscolaId);
            builder.HasMany(e => e.Instrutores).WithMany(e => e.AutoEscolas);
            builder.HasMany(e => e.Turmas).WithOne(e => e.AutoEscola).HasForeignKey(e => e.AutoEscolaId);
            builder.HasMany(e => e.Arquivos).WithOne(e => e.AutoEscola);
            builder.HasMany(e => e.Telefones).WithOne(e => e.AutoEscola);
        }
    }
}