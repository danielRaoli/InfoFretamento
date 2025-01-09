using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class addnomefantasia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cliente_NomeFantasia",
                table: "Pessoa",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                table: "Pessoa",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente_NomeFantasia",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                table: "Pessoa");
        }
    }
}
