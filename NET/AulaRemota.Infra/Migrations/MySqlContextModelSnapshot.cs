﻿// <auto-generated />
using System;
using AulaRemota.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AulaRemota.Infra.Migrations
{
    [DbContext(typeof(MySqlContext))]
    partial class MySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("AulaRemota.Core.Entity.Auth.AuthUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("AuthUser");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Edriving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Edriving");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.EdrivingCargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("EdrivingCargo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cargo = "DIRETOR"
                        },
                        new
                        {
                            Id = 2,
                            Cargo = "ANALISTA"
                        },
                        new
                        {
                            Id = 3,
                            Cargo = "ADMINISTRATIVO"
                        });
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("EnderecoLogradouro")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Uf")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Parceiro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Parceiro");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.ParceiroCargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ParceiroCargo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cargo = "DIRETOR"
                        },
                        new
                        {
                            Id = 2,
                            Cargo = "ANALISTA"
                        },
                        new
                        {
                            Id = 3,
                            Cargo = "ADMINISTRATIVO"
                        });
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("NivelAcesso")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Edriving", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.EdrivingCargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Parceiro", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.ParceiroCargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
