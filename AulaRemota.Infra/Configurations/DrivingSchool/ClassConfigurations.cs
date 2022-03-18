using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class ClassConfigurations : IEntityTypeConfiguration<TurmaModel>
    {
        public void Configure(EntityTypeBuilder<TurmaModel> builder)
        {
            builder.ToTable("class");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Code).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.StartDate).HasColumnType("datetime");
            builder.Property(e => e.EndDate).HasColumnType("datetime");
            builder.Property(e => e.DrivingSchoolId).HasColumnType("int").IsRequired();
            builder.Property(e => e.InstructorId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.DrivingSchool).WithMany(e => e.Classes).HasForeignKey(e => e.DrivingSchoolId);
            builder.HasMany(e => e.Courses).WithMany(e => e.Classes);
            builder.HasMany(e => e.Students).WithOne(e => e.Class).HasForeignKey(e => e.ClassId);
        }
    }
}