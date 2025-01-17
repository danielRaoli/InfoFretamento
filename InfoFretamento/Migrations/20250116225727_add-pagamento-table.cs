using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class addpagamentotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receitas_Pessoa_ResponsavelId",
                table: "Receitas");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_ResponsavelId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ValorPago",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "ResponsavelId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ValorParcial",
                table: "Receitas");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Receitas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValorPago = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ReceitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_ClienteId",
                table: "Receitas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_ReceitaId",
                table: "Pagamentos",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receitas_Pessoa_ClienteId",
                table: "Receitas",
                column: "ClienteId",
                principalTable: "Pessoa",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receitas_Pessoa_ClienteId",
                table: "Receitas");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_ClienteId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Receitas");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPago",
                table: "Viagens",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ResponsavelId",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorParcial",
                table: "Receitas",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_ResponsavelId",
                table: "Receitas",
                column: "ResponsavelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receitas_Pessoa_ResponsavelId",
                table: "Receitas",
                column: "ResponsavelId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
