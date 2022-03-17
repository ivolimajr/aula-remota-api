using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class InstructorConfigurations : IEntityTypeConfiguration<InstructorModel>
    {
        public void Configure(EntityTypeBuilder<InstructorModel> builder)
        {
            builder.ToTable("Instructor");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Identity).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Origin).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Birthdate).HasColumnType("datetime");
            builder.Property(e => e.AddressId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UserId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.User).WithOne(e => e.Instructor);
            builder.HasOne(e => e.Address).WithOne(e => e.Instructor);
            builder.HasMany(e => e.DrivingScools).WithMany(e => e.Instructors);
        }
    }
}