using AulaRemota.Core.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes.Auto_Escola
{
    class CursoConfiguracoes : IEntityTypeConfiguration<CursoModel>
    {
        public void Configure(EntityTypeBuilder<CursoModel> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Codigo).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.CargaHoraria).HasColumnType("int").IsRequired();
            builder.Property(e => e.AutoEscolaId).HasColumnType("int").IsRequired();
            builder.Property(e => e.InstrutorId).HasColumnType("int").IsRequired();
            builder.Property(e => e.TurmaId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.Instrutor).WithMany(e => e.Cursos).HasForeignKey(e => e.InstrutorId);
            builder.HasMany(e => e.Turmas).WithMany(e => e.Cursos);
            builder.HasMany(e => e.AutoEscolas).WithMany(e => e.Cursos);


        }
    }
}