using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class DrivingSchoolConfigurations : IEntityTypeConfiguration<DrivingSchoolModel>
    {
        public void Configure(EntityTypeBuilder<DrivingSchoolModel> builder)
        {
            builder.ToTable("drivingSchool");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CorporateName).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.FantasyName).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.StateRegistration).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Cnpj).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Site).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(e => e.FoundingDate).HasColumnType("datetime");
            builder.Property(e => e.AddressId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UserId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.User).WithOne(e => e.DrivingSchool);
            builder.HasOne(e => e.Address).WithOne(e => e.DrivingSchool);
            builder.HasMany(e => e.Administratives).WithOne(e => e.DrivingSchool).HasForeignKey(e => e.DrivingSchoolId);
            builder.HasMany(e => e.Instructors).WithMany(e => e.DrivingScools);
            builder.HasMany(e => e.Classes).WithOne(e => e.DrivingSchool).HasForeignKey(e => e.DrivingSchoolId);
            builder.HasMany(e => e.Files).WithOne(e => e.DrivingSchool);
            builder.HasMany(e => e.PhonesNumbers).WithOne(e => e.DrivingSchool);
        }
    }
}