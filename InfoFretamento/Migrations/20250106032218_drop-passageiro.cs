using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class droppassageiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passagens_Pessoa_PassageiroId",
                table: "Passagens");

            migrationBuilder.DropIndex(
                name: "IX_Passagens_PassageiroId",
                table: "Passagens");

            migrationBuilder.DropColumn(
                name: "Cartao",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "Pessoa");

            migrationBuilder.AddColumn<string>(
                name: "CpfPassageiro",
                table: "Passagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmailPassageiro",
                table: "Passagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NomePassageiro",
                table: "Passagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TelefonePassageiro",
                table: "Passagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpfPassageiro",
                table: "Passagens");

            migrationBuilder.DropColumn(
                name: "EmailPassageiro",
                table: "Passagens");

            migrationBuilder.DropColumn(
                name: "NomePassageiro",
                table: "Passagens");

            migrationBuilder.DropColumn(
                name: "TelefonePassageiro",
                table: "Passagens");

            migrationBuilder.AddColumn<string>(
                name: "Cartao",
                table: "Pessoa",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "Pessoa",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Passagens_PassageiroId",
                table: "Passagens",
                column: "PassageiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passagens_Pessoa_PassageiroId",
                table: "Passagens",
                column: "PassageiroId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
