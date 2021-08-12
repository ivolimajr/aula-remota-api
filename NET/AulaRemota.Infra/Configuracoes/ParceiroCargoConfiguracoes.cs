﻿using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    public class ParceiroCargoConfiguracoes : IEntityTypeConfiguration<ParceiroCargoModel>
    {
        public void Configure(EntityTypeBuilder<ParceiroCargoModel> builder)
        {
            builder.ToTable("ParceiroCargo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Cargo).HasColumnType("varchar").HasMaxLength(70).IsRequired();


            builder.HasData(
                new ParceiroCargoModel { Id = 1, Cargo = "DIRETOR" },
                new ParceiroCargoModel { Id = 2, Cargo = "ANALISTA" },
                new ParceiroCargoModel { Id = 3, Cargo = "ADMINISTRATIVO" }
            );

            builder.HasMany(e => e.Parceiros).WithOne(e => e.Cargo).HasForeignKey(e => e.CargoId);

            builder.HasIndex(e => e.Cargo);

        }
    }
}
