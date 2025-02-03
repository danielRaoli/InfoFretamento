using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class addboletopagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroDocumento",
                table: "Receitas");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPagamento",
                table: "Boletos",
                type: "DATE",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPagamento",
                table: "Abastecimentos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<string>(
                name: "NumeroDocumento",
                table: "Receitas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
