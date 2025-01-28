using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class removedatavencimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Pessoa_ClienteId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Receitas_Pessoa_ClienteId",
                table: "Receitas");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_ClienteId",
                table: "Receitas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_ClienteId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "CodigoNfe",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<string>(
                name: "ParadaPassageiro",
                table: "Passagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParadaPassageiro",
                table: "Passagens");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Receitas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Despesas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoNfe",
                table: "Abastecimentos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_ClienteId",
                table: "Receitas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_ClienteId",
                table: "Despesas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Pessoa_ClienteId",
                table: "Despesas",
                column: "ClienteId",
                principalTable: "Pessoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receitas_Pessoa_ClienteId",
                table: "Receitas",
                column: "ClienteId",
                principalTable: "Pessoa",
                principalColumn: "Id");
        }
    }
}
