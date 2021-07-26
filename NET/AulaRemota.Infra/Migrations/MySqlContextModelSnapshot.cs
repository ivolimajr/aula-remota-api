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

            modelBuilder.Entity("AulaRemota.Core.Entity.Auth.AuthUserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

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

            modelBuilder.Entity("AulaRemota.Core.Entity.AutoEscolaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArquivoId")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime>("DataFundacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("InscricaoEstadual")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Site")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AutoEscola");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AdministrativoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Aniversario")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Identidade")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Orgão")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Site")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("CargoId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Administrativo");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AlunoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Aniversario")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Identidade")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Orgão")
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.Property<int?>("TurmaModelId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("TurmaModelId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AutoEscolaCargoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("AutoEscolaCargo");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.CursoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<int>("CargaHoraria")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .HasColumnType("longtext");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int>("InstrutorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstrutorId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.InstrutorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Aniversario")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Identidade")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Orgão")
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Instrutor");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.TelefoneModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AdministrativoModelId")
                        .HasColumnType("int");

                    b.Property<int?>("AlunoModelId")
                        .HasColumnType("int");

                    b.Property<int?>("AutoEscolaModelId")
                        .HasColumnType("int");

                    b.Property<int?>("EdrivingModelId")
                        .HasColumnType("int");

                    b.Property<int?>("InstrutorModelId")
                        .HasColumnType("int");

                    b.Property<int?>("ParceiroModelId")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AdministrativoModelId");

                    b.HasIndex("AlunoModelId");

                    b.HasIndex("AutoEscolaModelId");

                    b.HasIndex("EdrivingModelId");

                    b.HasIndex("InstrutorModelId");

                    b.HasIndex("ParceiroModelId");

                    b.ToTable("Telefone");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.TurmaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.EdrivingCargoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(100)
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

            modelBuilder.Entity("AulaRemota.Core.Entity.EdrivingModel", b =>
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

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Edriving");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.EnderecoModel", b =>
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

            modelBuilder.Entity("AulaRemota.Core.Entity.ParceiroCargoModel", b =>
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

            modelBuilder.Entity("AulaRemota.Core.Entity.ParceiroModel", b =>
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

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Parceiro");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("NivelAcesso")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Models.ArquivoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AutoEscolaModelId")
                        .HasColumnType("int");

                    b.Property<string>("Destino")
                        .HasColumnType("longtext");

                    b.Property<string>("Formato")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaModelId");

                    b.ToTable("Arquivo");
                });

            modelBuilder.Entity("AutoEscolaModelCursoModel", b =>
                {
                    b.Property<int>("AutoEscolasId")
                        .HasColumnType("int");

                    b.Property<int>("CursosId")
                        .HasColumnType("int");

                    b.HasKey("AutoEscolasId", "CursosId");

                    b.HasIndex("CursosId");

                    b.ToTable("AutoEscolaModelCursoModel");
                });

            modelBuilder.Entity("AutoEscolaModelInstrutorModel", b =>
                {
                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<int>("InstrutoresId")
                        .HasColumnType("int");

                    b.HasKey("AutoEscolaId", "InstrutoresId");

                    b.HasIndex("InstrutoresId");

                    b.ToTable("AutoEscolaModelInstrutorModel");
                });

            modelBuilder.Entity("CursoModelTurmaModel", b =>
                {
                    b.Property<int>("CursosId")
                        .HasColumnType("int");

                    b.Property<int>("TurmasId")
                        .HasColumnType("int");

                    b.HasKey("CursosId", "TurmasId");

                    b.HasIndex("TurmasId");

                    b.ToTable("CursoModelTurmaModel");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.AutoEscolaModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.EnderecoModel", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AdministrativoModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", "AutoEscola")
                        .WithMany("Administrativos")
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.AutoEscolaCargoModel", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.EnderecoModel", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscola");

                    b.Navigation("Cargo");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AlunoModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", "AutoEscola")
                        .WithMany("Alunos")
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.EnderecoModel", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.TurmaModel", null)
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaModelId");

                    b.HasOne("AulaRemota.Core.Entity.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscola");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.CursoModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.InstrutorModel", "Instrutor")
                        .WithMany()
                        .HasForeignKey("InstrutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrutor");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.InstrutorModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.AutoEscolaCargoModel", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.EnderecoModel", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.TelefoneModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.AdministrativoModel", null)
                        .WithMany("Telefones")
                        .HasForeignKey("AdministrativoModelId");

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.AlunoModel", null)
                        .WithMany("Telefones")
                        .HasForeignKey("AlunoModelId");

                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", null)
                        .WithMany("Telefones")
                        .HasForeignKey("AutoEscolaModelId");

                    b.HasOne("AulaRemota.Core.Entity.EdrivingModel", null)
                        .WithMany("Telefones")
                        .HasForeignKey("EdrivingModelId");

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.InstrutorModel", null)
                        .WithMany("Telefones")
                        .HasForeignKey("InstrutorModelId");

                    b.HasOne("AulaRemota.Core.Entity.ParceiroModel", null)
                        .WithMany("Telefones")
                        .HasForeignKey("ParceiroModelId");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.TurmaModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", "AutoEscola")
                        .WithMany("Turmas")
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscola");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.EdrivingModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.EdrivingCargoModel", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.ParceiroModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.ParceiroCargoModel", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.EnderecoModel", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Core.Models.ArquivoModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", null)
                        .WithMany("Arquivos")
                        .HasForeignKey("AutoEscolaModelId");
                });

            modelBuilder.Entity("AutoEscolaModelCursoModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", null)
                        .WithMany()
                        .HasForeignKey("AutoEscolasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.CursoModel", null)
                        .WithMany()
                        .HasForeignKey("CursosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AutoEscolaModelInstrutorModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.AutoEscolaModel", null)
                        .WithMany()
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.InstrutorModel", null)
                        .WithMany()
                        .HasForeignKey("InstrutoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CursoModelTurmaModel", b =>
                {
                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.CursoModel", null)
                        .WithMany()
                        .HasForeignKey("CursosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Core.Entity.Auto_Escola.TurmaModel", null)
                        .WithMany()
                        .HasForeignKey("TurmasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.AutoEscolaModel", b =>
                {
                    b.Navigation("Administrativos");

                    b.Navigation("Alunos");

                    b.Navigation("Arquivos");

                    b.Navigation("Telefones");

                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AdministrativoModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.AlunoModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.InstrutorModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.Auto_Escola.TurmaModel", b =>
                {
                    b.Navigation("Alunos");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.EdrivingModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Core.Entity.ParceiroModel", b =>
                {
                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}
