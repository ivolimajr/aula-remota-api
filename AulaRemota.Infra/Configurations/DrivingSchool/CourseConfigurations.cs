using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class CourseConfigurations : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder.ToTable("course");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Code).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Workload).HasColumnType("int").IsRequired();
            builder.Property(e => e.DrivingSchoolId).HasColumnType("int").IsRequired();
            builder.Property(e => e.InstructorId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.DrivingSchool).WithMany(e => e.Courses);
            builder.HasOne(e => e.Instructor).WithMany(e => e.Courses).HasForeignKey(e => e.InstructorId);
            builder.HasMany(e => e.Classes).WithMany(e => e.Courses);
        }
    }
}