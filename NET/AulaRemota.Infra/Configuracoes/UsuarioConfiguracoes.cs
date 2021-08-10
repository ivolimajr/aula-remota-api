﻿using AulaRemota.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AulaRemota.Infra.Configuracoes
{
    public class UsuarioConfiguracoes : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Password).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
            builder.Property(e => e.status).HasColumnType("int").IsRequired();
            builder.Property(e => e.NivelAcesso).HasColumnType("int").IsRequired();

            builder.HasOne(e => e.AutoEscola).WithOne(e => e.Usuario);
            builder.HasOne(e => e.Administrativo).WithOne(e => e.Usuario);
            builder.HasOne(e => e.Aluno).WithOne(e => e.Usuario);
            builder.HasOne(e => e.Edriving).WithOne(e => e.Usuario);
            builder.HasOne(e => e.Parceiro).WithOne(e => e.Usuario);
            builder.HasOne(e => e.Instrutor).WithOne(e => e.Usuario);

            builder.HasIndex(e => e.Nome);


        }
    }
}