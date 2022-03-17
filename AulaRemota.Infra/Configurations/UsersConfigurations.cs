using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AulaRemota.Infra.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("user");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Password).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.Status).HasColumnType("int").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValue(DateTime.Now).IsRequired();
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");

            builder.HasOne(e => e.DrivingSchool).WithOne(e => e.User);
            builder.HasOne(e => e.Administrative).WithOne(e => e.User);
            builder.HasOne(e => e.Student).WithOne(e => e.User);
            builder.HasOne(e => e.Edriving).WithOne(e => e.User);
            builder.HasOne(e => e.Partnner).WithOne(e => e.User);
            builder.HasOne(e => e.Instructor).WithOne(e => e.User);
            builder.HasMany(e => e.Roles);

            builder.HasIndex(e => e.Name);
        }
    }
}
