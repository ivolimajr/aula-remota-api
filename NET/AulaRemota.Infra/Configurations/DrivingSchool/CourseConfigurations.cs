using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class CourseConfigurations : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Codigo).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.CargaHoraria).HasColumnType("int").IsRequired();
            builder.Property(e => e.AutoEscolaId).HasColumnType("int").IsRequired();
            builder.Property(e => e.InstrutorId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.AutoEscolas).WithMany(e => e.Cursos);
            builder.HasOne(e => e.Instrutor).WithMany(e => e.Cursos).HasForeignKey(e => e.InstrutorId);
            builder.HasMany(e => e.Turmas).WithMany(e => e.Cursos);
        }
    }
}