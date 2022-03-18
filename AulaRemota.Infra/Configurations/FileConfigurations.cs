using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configurations
{
    public class FileConfigurations : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.ToTable("file");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FileName).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Destiny).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Extension).HasColumnType("varchar").HasMaxLength(10).IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.DrivingSchool).WithMany(e => e.Files);
            builder.HasOne(e => e.Instructor).WithMany(e => e.Files);
        }
    }
}