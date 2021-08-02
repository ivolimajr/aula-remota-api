using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes.Auto_Escola
{
    public class TurmaConfiguracoes : IEntityTypeConfiguration<TurmaModel>
    {
        public void Configure(EntityTypeBuilder<TurmaModel> builder)
        {
            builder.ToTable("Turma");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Codigo).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.DataInicio).HasColumnType("datetime");
            builder.Property(e => e.DataFim).HasColumnType("datetime");
            builder.Property(e => e.AutoEscolaId).HasColumnType("int").IsRequired();
            builder.Property(e => e.InstrutorId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.AutoEscola).WithMany(e => e.Turmas).HasForeignKey(e => e.AutoEscolaId);
            builder.HasMany(e => e.Cursos).WithMany(e => e.Turmas);
            builder.HasMany(e => e.Alunos).WithOne(e => e.Turma).HasForeignKey(e => e.TurmaId);
        }
    }
}