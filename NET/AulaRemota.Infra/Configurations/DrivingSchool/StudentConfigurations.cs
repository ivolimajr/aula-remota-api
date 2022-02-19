using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class StudentConfigurations : IEntityTypeConfiguration<StudentModel>
    {
        public void Configure(EntityTypeBuilder<StudentModel> builder)
        {
            builder.ToTable("Aluno");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Identidade).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Orgao).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Aniversario).HasColumnType("datetime");
            builder.Property(e => e.TurmaId).HasColumnType("int").IsRequired();
            builder.Property(e => e.AutoEscolaId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.Turma).WithMany(e => e.Alunos).HasForeignKey(e => e.TurmaId);
            builder.HasOne(e => e.AutoEscola).WithMany(e => e.Alunos).HasForeignKey(e => e.AutoEscolaId);
            builder.HasOne(e => e.Endereco).WithMany(e => e.Alunos).HasForeignKey(e => e.EnderecoId);
            builder.HasOne(e => e.Usuario).WithOne(e => e.Aluno);
            builder.HasMany(e => e.Telefones).WithOne(e => e.Aluno);
        }
    }
}