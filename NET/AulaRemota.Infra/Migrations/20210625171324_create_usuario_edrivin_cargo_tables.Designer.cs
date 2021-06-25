﻿// <auto-generated />
using AulaRemota.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AulaRemota.Infra.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20210625171324_create_usuario_edrivin_cargo_tables")]
    partial class create_usuario_edrivin_cargo_tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AulaRemota.Core.Entity.Edriving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("FullName")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cargo")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("EdrivingCargo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cargo = "Diretor"
                        },
                        new
                        {
                            Id = 2,
                            Cargo = "Analista"
                        },
                        new
                        {
                            Id = 3,
                            Cargo = "Administrativo"
                        });
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("FullName")
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
#pragma warning restore 612, 618
        }
    }
}
