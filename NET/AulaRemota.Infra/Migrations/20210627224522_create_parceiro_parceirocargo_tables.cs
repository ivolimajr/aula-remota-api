using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AulaRemota.Infra.Migrations
{
    public partial class create_parceiro_parceirocargo_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParceiroCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cargo = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParceiroCargo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parceiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(70)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpnpj = table.Column<string>(type: "varchar(14)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parceiro_ParceiroCargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "ParceiroCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parceiro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "EdrivingCargo",
                keyColumn: "Id",
                keyValue: 1,
                column: "Cargo",
                value: "DIRETOR");

            migrationBuilder.UpdateData(
                table: "EdrivingCargo",
                keyColumn: "Id",
                keyValue: 2,
                column: "Cargo",
                value: "ANALISTA");

            migrationBuilder.UpdateData(
                table: "EdrivingCargo",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cargo",
                value: "ADMINISTRATIVO");

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
                name: "IX_Parceiro_CargoId",
                table: "Parceiro",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_UsuarioId",
                table: "Parceiro",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parceiro");

            migrationBuilder.DropTable(
                name: "ParceiroCargo");

            migrationBuilder.UpdateData(
                table: "EdrivingCargo",
                keyColumn: "Id",
                keyValue: 1,
                column: "Cargo",
                value: "Diretor");

            migrationBuilder.UpdateData(
                table: "EdrivingCargo",
                keyColumn: "Id",
                keyValue: 2,
                column: "Cargo",
                value: "Analista");

            migrationBuilder.UpdateData(
                table: "EdrivingCargo",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cargo",
                value: "Administrativo");
        }
    }
}
