using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class StudentConfigurations : IEntityTypeConfiguration<StudentModel>
    {
        public void Configure(EntityTypeBuilder<StudentModel> builder)
        {
            builder.ToTable("student");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Identity).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Origin).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Birthdate).HasColumnType("datetime");
            builder.Property(e => e.ClassId).HasColumnType("int").IsRequired();
            builder.Property(e => e.DrivingSchoolId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.Class).WithMany(e => e.Students).HasForeignKey(e => e.ClassId);
            builder.HasOne(e => e.DrivingSchool).WithMany(e => e.Sudents).HasForeignKey(e => e.DrivingSchoolId);
            builder.HasOne(e => e.Address).WithMany(e => e.Students).HasForeignKey(e => e.AddressId);
            builder.HasOne(e => e.User).WithOne(e => e.Student);
            builder.HasMany(e => e.PhonesNumbers).WithOne(e => e.Student);
        }
    }
}