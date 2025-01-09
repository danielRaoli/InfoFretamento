using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class changepassagemtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassageiroId",
                table: "Passagens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassageiroId",
                table: "Passagens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
