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
            builder.ToTable("Arquivo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Destino).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Formato).HasColumnType("varchar").HasMaxLength(10).IsRequired();

            //RELACIONAMENTOS
            builder.HasOne(e => e.AutoEscola).WithMany(e => e.Arquivos);
            builder.HasOne(e => e.Instrutor).WithMany(e => e.Arquivos);
        }
    }
}