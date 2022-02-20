using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations.DrivingSchool
{
    public class AdministrativeConfigurations : IEntityTypeConfiguration<AdministrativeModel>
    {
        public void Configure(EntityTypeBuilder<AdministrativeModel> builder)
        {
            builder.ToTable("Administrative");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Cpf).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            builder.Property(e => e.Identity).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Origin).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Birthdate).HasColumnType("datetime");
            builder.Property(e => e.AddressId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UserId).HasColumnType("int").IsRequired();
            builder.Property(e => e.DrivingSchoolId).HasColumnType("int").IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.User).WithOne(e => e.Administrative);
            builder.HasOne(e => e.Address).WithOne(e => e.Administrative);
            builder.HasOne(e => e.DrivingSchool).WithMany(e => e.Administratives).HasForeignKey(e => e.DrivingSchoolId);
        }
    }
}