using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class feriastable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colaborador_FimFerias",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Colaborador_InicioFerias",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "FimFerias",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "InicioFerias",
                table: "Pessoa");

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "Retiradas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ferias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ResponsavelId = table.Column<int>(type: "int", nullable: false),
                    InicioFerias = table.Column<DateOnly>(type: "DATE", nullable: false),
                    FimFerias = table.Column<DateOnly>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ferias_Pessoa_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Retiradas_VeiculoId",
                table: "Retiradas",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ferias_ResponsavelId",
                table: "Ferias",
                column: "ResponsavelId");


            migrationBuilder.AddForeignKey(
                name: "FK_Retiradas_Veiculos_VeiculoId",
                table: "Retiradas",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Retiradas_Veiculos_VeiculoId",
                table: "Retiradas");

            migrationBuilder.DropTable(
                name: "Ferias");

            migrationBuilder.DropIndex(
                name: "IX_Retiradas_VeiculoId",
                table: "Retiradas");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Retiradas");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Colaborador_FimFerias",
                table: "Pessoa",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Colaborador_InicioFerias",
                table: "Pessoa",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FimFerias",
                table: "Pessoa",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "InicioFerias",
                table: "Pessoa",
                type: "date",
                nullable: true);
        }
    }
}
