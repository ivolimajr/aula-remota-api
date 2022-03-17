using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class PhonesConfigurations : IEntityTypeConfiguration<PhoneModel>
    {
        public void Configure(EntityTypeBuilder<PhoneModel> builder)
        {
            builder.ToTable("phones");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar").HasMaxLength(20).IsRequired();

            builder.HasOne(e => e.Administrative).WithMany(e => e.PhonesNumbers);
            builder.HasOne(e => e.Partnner).WithMany(e => e.PhonesNumbers);
            builder.HasOne(e => e.Instructor).WithMany(e => e.PhonesNumbers);
            builder.HasOne(e => e.Edriving).WithMany(e => e.PhonesNumbers);
            builder.HasOne(e => e.DrivingSchool).WithMany(e => e.PhonesNumbers);
            builder.HasOne(e => e.Student).WithMany(e => e.PhonesNumbers);

        }
    }
}
