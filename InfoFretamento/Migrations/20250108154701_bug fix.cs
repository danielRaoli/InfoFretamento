using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class bugfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ferias_Pessoa_ResponsavelId1",
                table: "Ferias");

            migrationBuilder.DropIndex(
                name: "IX_Ferias_ResponsavelId1",
                table: "Ferias");

            migrationBuilder.DropColumn(
                name: "ResponsavelId1",
                table: "Ferias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsavelId1",
                table: "Ferias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ferias_ResponsavelId1",
                table: "Ferias",
                column: "ResponsavelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Pessoa_ResponsavelId1",
                table: "Ferias",
                column: "ResponsavelId1",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
