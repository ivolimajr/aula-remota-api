using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AulaRemota.Infra.Migrations
{
    public partial class addressajustecomplemento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "AddressNumber",
                table: "Address",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Address",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressNumber",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Address",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
