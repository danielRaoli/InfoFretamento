using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class removeabastecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<string>(
                name: "OrigemPagamento",
                table: "Abastecimentos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrigemPagamento",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPagamento",
                table: "Abastecimentos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
