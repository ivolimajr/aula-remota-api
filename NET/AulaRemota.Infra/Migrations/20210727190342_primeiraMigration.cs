using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AulaRemota.Infra.Migrations
{
    public partial class primeiraMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "apiUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apiUser", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoEscolaCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cargo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoEscolaCargo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EdrivingCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cargo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdrivingCargo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Uf = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnderecoLogradouro = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParceiroCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cargo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParceiroCargo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false),
                    NivelAcesso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoEscola",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RazaoSocial = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeFantasia = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InscricaoEstadual = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataFundacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Site = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoEscola", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoEscola_AutoEscolaCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "AutoEscolaCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoEscola_endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoEscola_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Edriving",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edriving", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edriving_EdrivingCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "EdrivingCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Edriving_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instrutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Identidade = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Orgão = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aniversario = table.Column<DateTime>(type: "datetime", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instrutor_AutoEscolaCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "AutoEscolaCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instrutor_endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instrutor_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parceiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parceiro_endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parceiro_ParceiroCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "ParceiroCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parceiro_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Administrativo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Identidade = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Orgão = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aniversario = table.Column<DateTime>(type: "datetime", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AutoEscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrativo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrativo_AutoEscola_AutoEscolaId",
                        column: x => x.AutoEscolaId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrativo_AutoEscolaCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "AutoEscolaCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrativo_endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrativo_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Arquivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Formato = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Destino = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AutoEscolaId = table.Column<int>(type: "int", nullable: true),
                    InstrutorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivo_AutoEscola_AutoEscolaId",
                        column: x => x.AutoEscolaId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arquivo_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoEscolaModelInstrutorModel",
                columns: table => new
                {
                    AutoEscolasId = table.Column<int>(type: "int", nullable: false),
                    InstrutoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoEscolaModelInstrutorModel", x => new { x.AutoEscolasId, x.InstrutoresId });
                    table.ForeignKey(
                        name: "FK_AutoEscolaModelInstrutorModel_AutoEscola_AutoEscolasId",
                        column: x => x.AutoEscolasId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoEscolaModelInstrutorModel_Instrutor_InstrutoresId",
                        column: x => x.InstrutoresId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InstrutorId = table.Column<int>(type: "int", nullable: false),
                    AutoEscolaId = table.Column<int>(type: "int", nullable: false),
                    AutoEscolasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_AutoEscola_AutoEscolasId",
                        column: x => x.AutoEscolasId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Curso_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime", nullable: false),
                    AutoEscolaId = table.Column<int>(type: "int", nullable: false),
                    InstrutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_AutoEscola_AutoEscolaId",
                        column: x => x.AutoEscolaId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turma_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Identidade = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Orgao = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aniversario = table.Column<DateTime>(type: "datetime", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    AutoEscolaId = table.Column<int>(type: "int", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_AutoEscola_AutoEscolaId",
                        column: x => x.AutoEscolaId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CursoModelTurmaModel",
                columns: table => new
                {
                    CursosId = table.Column<int>(type: "int", nullable: false),
                    TurmasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoModelTurmaModel", x => new { x.CursosId, x.TurmasId });
                    table.ForeignKey(
                        name: "FK_CursoModelTurmaModel_Curso_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoModelTurmaModel_Turma_TurmasId",
                        column: x => x.TurmasId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "telefone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EdrivingId = table.Column<int>(type: "int", nullable: true),
                    ParceiroId = table.Column<int>(type: "int", nullable: true),
                    AutoEscolaId = table.Column<int>(type: "int", nullable: true),
                    AdministrativoId = table.Column<int>(type: "int", nullable: true),
                    InstrutorId = table.Column<int>(type: "int", nullable: true),
                    AlunoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_telefone_Administrativo_AdministrativoId",
                        column: x => x.AdministrativoId,
                        principalTable: "Administrativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_telefone_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_telefone_AutoEscola_AutoEscolaId",
                        column: x => x.AutoEscolaId,
                        principalTable: "AutoEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_telefone_Edriving_EdrivingId",
                        column: x => x.EdrivingId,
                        principalTable: "Edriving",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_telefone_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_telefone_Parceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "EdrivingCargo",
                columns: new[] { "Id", "Cargo" },
                values: new object[,]
                {
                    { 1, "DIRETOR" },
                    { 2, "ANALISTA" },
                    { 3, "ADMINISTRATIVO" }
                });

            migrationBuilder.InsertData(
                table: "ParceiroCargo",
                columns: new[] { "Id", "Cargo" },
                values: new object[,]
                {
                    { 1, "DIRETOR" },
                    { 2, "ANALISTA" },
                    { 3, "ADMINISTRATIVO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrativo_AutoEscolaId",
                table: "Administrativo",
                column: "AutoEscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrativo_CargoId",
                table: "Administrativo",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrativo_EnderecoId",
                table: "Administrativo",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrativo_UsuarioId",
                table: "Administrativo",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_AutoEscolaId",
                table: "Aluno",
                column: "AutoEscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EnderecoId",
                table: "Aluno",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_UsuarioId",
                table: "Aluno",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_AutoEscolaId",
                table: "Arquivo",
                column: "AutoEscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_InstrutorId",
                table: "Arquivo",
                column: "InstrutorId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoEscola_CargoId",
                table: "AutoEscola",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoEscola_EnderecoId",
                table: "AutoEscola",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoEscola_UsuarioId",
                table: "AutoEscola",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoEscolaModelInstrutorModel_InstrutoresId",
                table: "AutoEscolaModelInstrutorModel",
                column: "InstrutoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_AutoEscolasId",
                table: "Curso",
                column: "AutoEscolasId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_InstrutorId",
                table: "Curso",
                column: "InstrutorId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoModelTurmaModel_TurmasId",
                table: "CursoModelTurmaModel",
                column: "TurmasId");

            migrationBuilder.CreateIndex(
                name: "IX_Edriving_CargoId",
                table: "Edriving",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Edriving_Nome",
                table: "Edriving",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Edriving_UsuarioId",
                table: "Edriving",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EdrivingCargo_Cargo",
                table: "EdrivingCargo",
                column: "Cargo");

            migrationBuilder.CreateIndex(
                name: "IX_Instrutor_CargoId",
                table: "Instrutor",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrutor_EnderecoId",
                table: "Instrutor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instrutor_UsuarioId",
                table: "Instrutor",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_CargoId",
                table: "Parceiro",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_EnderecoId",
                table: "Parceiro",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_Nome",
                table: "Parceiro",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_UsuarioId",
                table: "Parceiro",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParceiroCargo_Cargo",
                table: "ParceiroCargo",
                column: "Cargo");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_AdministrativoId",
                table: "telefone",
                column: "AdministrativoId");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_AlunoId",
                table: "telefone",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_AutoEscolaId",
                table: "telefone",
                column: "AutoEscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_EdrivingId",
                table: "telefone",
                column: "EdrivingId");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_InstrutorId",
                table: "telefone",
                column: "InstrutorId");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_ParceiroId",
                table: "telefone",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_AutoEscolaId",
                table: "Turma",
                column: "AutoEscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_InstrutorId",
                table: "Turma",
                column: "InstrutorId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Nome",
                table: "usuario",
                column: "Nome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apiUser");

            migrationBuilder.DropTable(
                name: "Arquivo");

            migrationBuilder.DropTable(
                name: "AutoEscolaModelInstrutorModel");

            migrationBuilder.DropTable(
                name: "CursoModelTurmaModel");

            migrationBuilder.DropTable(
                name: "telefone");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Administrativo");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Edriving");

            migrationBuilder.DropTable(
                name: "Parceiro");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "EdrivingCargo");

            migrationBuilder.DropTable(
                name: "ParceiroCargo");

            migrationBuilder.DropTable(
                name: "AutoEscola");

            migrationBuilder.DropTable(
                name: "Instrutor");

            migrationBuilder.DropTable(
                name: "AutoEscolaCargo");

            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
