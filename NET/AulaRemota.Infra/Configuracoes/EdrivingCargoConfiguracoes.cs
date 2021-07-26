using AulaRemota.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    class EdrivingCargoConfiguracoes : IEntityTypeConfiguration<EdrivingCargoModel>
    {
        public void Configure(EntityTypeBuilder<EdrivingCargoModel> builder)
        {
            builder.ToTable("EdrivingCargo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Cargo).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            builder.HasIndex(e => e.Cargo);

            builder.HasData(
                new EdrivingCargoModel { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargoModel { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargoModel { Id = 3, Cargo = "ADMINISTRATIVO" }
            );

        }
    }
}
