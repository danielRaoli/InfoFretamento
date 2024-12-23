using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class updatetablerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Pessoa_ClienteId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Pessoa_FornecedorId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Pessoa_MotoristaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_ClienteId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_FornecedorId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_MotoristaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "MotoristaId",
                table: "Despesas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Despesas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "Despesas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotoristaId",
                table: "Despesas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_ClienteId",
                table: "Despesas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_FornecedorId",
                table: "Despesas",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_MotoristaId",
                table: "Despesas",
                column: "MotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Pessoa_ClienteId",
                table: "Despesas",
                column: "ClienteId",
                principalTable: "Pessoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Pessoa_FornecedorId",
                table: "Despesas",
                column: "FornecedorId",
                principalTable: "Pessoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Pessoa_MotoristaId",
                table: "Despesas",
                column: "MotoristaId",
                principalTable: "Pessoa",
                principalColumn: "Id");
        }
    }
}
