using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    public class EdrivingCargoConfiguracoes : IEntityTypeConfiguration<EdrivingCargoModel>
    {
        public void Configure(EntityTypeBuilder<EdrivingCargoModel> builder)
        {
            builder.ToTable("EdrivingCargo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Cargo).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            builder.HasData(
                new EdrivingCargoModel { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargoModel { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargoModel { Id = 3, Cargo = "ADMINISTRATIVO" }
            );

            builder.HasIndex(e => e.Cargo);

            builder.HasMany(e => e.Edrivings).WithOne(e => e.Cargo).HasForeignKey(e => e.CargoId);

        }
    }
}
