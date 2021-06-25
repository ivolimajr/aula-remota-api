using Microsoft.EntityFrameworkCore.Migrations;

namespace AulaRemota.Infra.Migrations
{
    public partial class create_usuario_edrivin_cargo_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EdrivingCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdrivingCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(70)", nullable: true),
                    Password = table.Column<string>(type: "varchar(150)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    NivelAcesso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Edriving",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: true),
                    Email = table.Column<string>(type: "varchar(70)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true),
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
                        name: "FK_Edriving_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EdrivingCargo",
                columns: new[] { "Id", "Cargo" },
                values: new object[] { 1, "Diretor" });

            migrationBuilder.InsertData(
                table: "EdrivingCargo",
                columns: new[] { "Id", "Cargo" },
                values: new object[] { 2, "Analista" });

            migrationBuilder.InsertData(
                table: "EdrivingCargo",
                columns: new[] { "Id", "Cargo" },
                values: new object[] { 3, "Administrativo" });

            migrationBuilder.CreateIndex(
                name: "IX_Edriving_CargoId",
                table: "Edriving",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Edriving_UsuarioId",
                table: "Edriving",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edriving");

            migrationBuilder.DropTable(
                name: "EdrivingCargo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
