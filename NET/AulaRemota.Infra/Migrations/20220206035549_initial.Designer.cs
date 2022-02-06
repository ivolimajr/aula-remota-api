﻿// <auto-generated />
using System;
using AulaRemota.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AulaRemota.Infra.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20220206035549_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ApiUserModelRolesModel", b =>
                {
                    b.Property<int>("ApiUsersId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("ApiUsersId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ApiUserModelRolesModel");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auth.ApiUserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("ApiUser");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AdministrativoModel", b =>
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
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Orgão")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Administrativo");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AlunoModel", b =>
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

                    b.Property<string>("Orgao")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("TurmaId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("AutoEscola");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.CursoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<int?>("AutoEscolasId")
                        .HasColumnType("int");

                    b.Property<int>("CargaHoraria")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("InstrutorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolasId");

                    b.HasIndex("InstrutorId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Aniversario")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Identidade")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Orgão")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Instrutor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.TelefoneModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AdministrativoId")
                        .HasColumnType("int");

                    b.Property<int?>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int?>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<int?>("EdrivingId")
                        .HasColumnType("int");

                    b.Property<int?>("InstrutorId")
                        .HasColumnType("int");

                    b.Property<int?>("ParceiroId")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("AdministrativoId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("EdrivingId");

                    b.HasIndex("InstrutorId");

                    b.HasIndex("ParceiroId");

                    b.ToTable("Telefone");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.TurmaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InstrutorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("InstrutorId");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingCargoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.HasKey("Id");

                    b.ToTable("EdrivingCargo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cargo = "ADMINISTRATIVO"
                        },
                        new
                        {
                            Id = 2,
                            Cargo = "ANALISTA"
                        },
                        new
                        {
                            Id = 3,
                            Cargo = "DIRETOR"
                        });
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Edriving");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EnderecoModel", b =>
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

            modelBuilder.Entity("AulaRemota.Infra.Entity.ParceiroCargoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.HasKey("Id");

                    b.ToTable("ParceiroCargo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cargo = "ADMINISTRATIVO"
                        },
                        new
                        {
                            Id = 2,
                            Cargo = "ANALISTA"
                        },
                        new
                        {
                            Id = 3,
                            Cargo = "DIRETOR"
                        });
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.ParceiroModel", b =>
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
                        .HasColumnType("varchar(150)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Parceiro");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.RolesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "ADMINISTRATIVO"
                        },
                        new
                        {
                            Id = 2,
                            Role = "ALUNO"
                        },
                        new
                        {
                            Id = 3,
                            Role = "APIUSER"
                        },
                        new
                        {
                            Id = 4,
                            Role = "AUTOESCOLA"
                        },
                        new
                        {
                            Id = 5,
                            Role = "EDRIVING"
                        },
                        new
                        {
                            Id = 6,
                            Role = "INSTRUTOR"
                        },
                        new
                        {
                            Id = 7,
                            Role = "PARCEIRO"
                        });
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("NivelAcesso")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Models.ArquivoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AutoEscolaId")
                        .HasColumnType("int");

                    b.Property<string>("Destino")
                        .HasColumnType("longtext");

                    b.Property<string>("Formato")
                        .HasColumnType("longtext");

                    b.Property<int?>("InstrutorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AutoEscolaId");

                    b.HasIndex("InstrutorId");

                    b.ToTable("Arquivo");
                });

            modelBuilder.Entity("AutoEscolaModelInstrutorModel", b =>
                {
                    b.Property<int>("AutoEscolasId")
                        .HasColumnType("int");

                    b.Property<int>("InstrutoresId")
                        .HasColumnType("int");

                    b.HasKey("AutoEscolasId", "InstrutoresId");

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

            modelBuilder.Entity("RolesModelUsuarioModel", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("RolesModelUsuarioModel");
                });

            modelBuilder.Entity("ApiUserModelRolesModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auth.ApiUserModel", null)
                        .WithMany()
                        .HasForeignKey("ApiUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.RolesModel", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AdministrativoModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "AutoEscola")
                        .WithMany("Administrativos")
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.EnderecoModel", "Endereco")
                        .WithOne("Administrativo")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.AdministrativoModel", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", "Usuario")
                        .WithOne("Administrativo")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.AdministrativoModel", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscola");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AlunoModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "AutoEscola")
                        .WithMany("Alunos")
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.EnderecoModel", "Endereco")
                        .WithMany("Alunos")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.TurmaModel", "Turma")
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", "Usuario")
                        .WithOne("Aluno")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.AlunoModel", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscola");

                    b.Navigation("Endereco");

                    b.Navigation("Turma");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.EnderecoModel", "Endereco")
                        .WithOne("AutoEscola")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", "Usuario")
                        .WithOne("AutoEscola")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.CursoModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "AutoEscolas")
                        .WithMany("Cursos")
                        .HasForeignKey("AutoEscolasId");

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", "Instrutor")
                        .WithMany("Cursos")
                        .HasForeignKey("InstrutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscolas");

                    b.Navigation("Instrutor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.EnderecoModel", "Endereco")
                        .WithOne("Instrutor")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", "Usuario")
                        .WithOne("Instrutor")
                        .HasForeignKey("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.TelefoneModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AdministrativoModel", "Administrativo")
                        .WithMany("Telefones")
                        .HasForeignKey("AdministrativoId");

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AlunoModel", "Aluno")
                        .WithMany("Telefones")
                        .HasForeignKey("AlunoId");

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "AutoEscola")
                        .WithMany("Telefones")
                        .HasForeignKey("AutoEscolaId");

                    b.HasOne("AulaRemota.Infra.Entity.EdrivingModel", "Edriving")
                        .WithMany("Telefones")
                        .HasForeignKey("EdrivingId");

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", "Instrutor")
                        .WithMany("Telefones")
                        .HasForeignKey("InstrutorId");

                    b.HasOne("AulaRemota.Infra.Entity.ParceiroModel", "Parceiro")
                        .WithMany("Telefones")
                        .HasForeignKey("ParceiroId");

                    b.Navigation("Administrativo");

                    b.Navigation("Aluno");

                    b.Navigation("AutoEscola");

                    b.Navigation("Edriving");

                    b.Navigation("Instrutor");

                    b.Navigation("Parceiro");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.TurmaModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "AutoEscola")
                        .WithMany("Turmas")
                        .HasForeignKey("AutoEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", "Instrutor")
                        .WithMany()
                        .HasForeignKey("InstrutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoEscola");

                    b.Navigation("Instrutor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.EdrivingCargoModel", "Cargo")
                        .WithMany("Edrivings")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", "Usuario")
                        .WithOne("Edriving")
                        .HasForeignKey("AulaRemota.Infra.Entity.EdrivingModel", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.ParceiroModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.ParceiroCargoModel", "Cargo")
                        .WithMany("Parceiros")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.EnderecoModel", "Endereco")
                        .WithOne("Parceiro")
                        .HasForeignKey("AulaRemota.Infra.Entity.ParceiroModel", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", "Usuario")
                        .WithOne("Parceiro")
                        .HasForeignKey("AulaRemota.Infra.Entity.ParceiroModel", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Endereco");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AulaRemota.Infra.Models.ArquivoModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", "AutoEscola")
                        .WithMany("Arquivos")
                        .HasForeignKey("AutoEscolaId");

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", "Instrutor")
                        .WithMany("Arquivos")
                        .HasForeignKey("InstrutorId");

                    b.Navigation("AutoEscola");

                    b.Navigation("Instrutor");
                });

            modelBuilder.Entity("AutoEscolaModelInstrutorModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", null)
                        .WithMany()
                        .HasForeignKey("AutoEscolasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", null)
                        .WithMany()
                        .HasForeignKey("InstrutoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CursoModelTurmaModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.CursoModel", null)
                        .WithMany()
                        .HasForeignKey("CursosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.Auto_Escola.TurmaModel", null)
                        .WithMany()
                        .HasForeignKey("TurmasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolesModelUsuarioModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.RolesModel", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UsuarioModel", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AdministrativoModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AlunoModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.AutoEscolaModel", b =>
                {
                    b.Navigation("Administrativos");

                    b.Navigation("Alunos");

                    b.Navigation("Arquivos");

                    b.Navigation("Cursos");

                    b.Navigation("Telefones");

                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.InstrutorModel", b =>
                {
                    b.Navigation("Arquivos");

                    b.Navigation("Cursos");

                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auto_Escola.TurmaModel", b =>
                {
                    b.Navigation("Alunos");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingCargoModel", b =>
                {
                    b.Navigation("Edrivings");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EnderecoModel", b =>
                {
                    b.Navigation("Administrativo");

                    b.Navigation("Alunos");

                    b.Navigation("AutoEscola");

                    b.Navigation("Instrutor");

                    b.Navigation("Parceiro");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.ParceiroCargoModel", b =>
                {
                    b.Navigation("Parceiros");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.ParceiroModel", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.UsuarioModel", b =>
                {
                    b.Navigation("Administrativo");

                    b.Navigation("Aluno");

                    b.Navigation("AutoEscola");

                    b.Navigation("Edriving");

                    b.Navigation("Instrutor");

                    b.Navigation("Parceiro");
                });
#pragma warning restore 612, 618
        }
    }
}
