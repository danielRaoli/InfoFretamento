using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class addpoltronavolta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poltrona",
                table: "Passagens");

            migrationBuilder.AddColumn<int>(
                name: "PoltronaIda",
                table: "Passagens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PoltronaVolta",
                table: "Passagens",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoltronaIda",
                table: "Passagens");

            migrationBuilder.DropColumn(
                name: "PoltronaVolta",
                table: "Passagens");

            migrationBuilder.AddColumn<int>(
                name: "Poltrona",
                table: "Passagens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
