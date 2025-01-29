using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class updatesalario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSalario",
                table: "Salario");

            migrationBuilder.DropColumn(
                name: "DataVale",
                table: "Salario");

            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "DespesaMensal");

            migrationBuilder.AddColumn<int>(
                name: "DiaSalario",
                table: "Salario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiaVale",
                table: "Salario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiaPagamento",
                table: "DespesaMensal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaSalario",
                table: "Salario");

            migrationBuilder.DropColumn(
                name: "DiaVale",
                table: "Salario");

            migrationBuilder.DropColumn(
                name: "DiaPagamento",
                table: "DespesaMensal");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataSalario",
                table: "Salario",
                type: "DATE",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataVale",
                table: "Salario",
                type: "DATE",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPagamento",
                table: "DespesaMensal",
                type: "DATE",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
