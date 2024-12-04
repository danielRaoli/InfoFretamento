using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class manutentionservicestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRetorno",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "DataSaida",
                table: "Viagens");

            migrationBuilder.RenameColumn(
                name: "HorarioSaida",
                table: "Viagens",
                newName: "DataHorarioSaida_Hora");

            migrationBuilder.RenameColumn(
                name: "HorarioRetorno",
                table: "Viagens",
                newName: "DataHorarioSaidaGaragem_Hora");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHorarioChegada_Data",
                table: "Viagens",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DataHorarioChegada_Hora",
                table: "Viagens",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHorarioRetorno_Data",
                table: "Viagens",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DataHorarioRetorno_Hora",
                table: "Viagens",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHorarioSaidaGaragem_Data",
                table: "Viagens",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHorarioSaida_Data",
                table: "Viagens",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Itinerario",
                table: "Viagens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Parcelas",
                table: "Viagens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoPagamento",
                table: "Viagens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoViagem",
                table: "Viagens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorContratado",
                table: "Viagens",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDespesas",
                table: "Viagens",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPago",
                table: "Viagens",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FormaPagamento",
                table: "Despesas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PagamentoParcial",
                table: "Despesas",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ViagemId",
                table: "Despesas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeServico = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manutencoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataLancamento = table.Column<DateTime>(type: "date", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataRealizada = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ServicoId = table.Column<int>(type: "integer", nullable: false),
                    VeiculoId = table.Column<int>(type: "integer", nullable: false),
                    KmPrevista = table.Column<int>(type: "integer", nullable: false),
                    KmAtual = table.Column<int>(type: "integer", nullable: false),
                    KmRealizada = table.Column<int>(type: "integer", nullable: false),
                    Custo = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutencoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_ViagemId",
                table: "Despesas",
                column: "ViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_ServicoId",
                table: "Manutencoes",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_VeiculoId",
                table: "Manutencoes",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Viagens_ViagemId",
                table: "Despesas",
                column: "ViagemId",
                principalTable: "Viagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Viagens_ViagemId",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "Manutencoes");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_ViagemId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DataHorarioChegada_Data",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "DataHorarioChegada_Hora",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "DataHorarioRetorno_Data",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "DataHorarioRetorno_Hora",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "DataHorarioSaidaGaragem_Data",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "DataHorarioSaida_Data",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "Itinerario",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "Parcelas",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "TipoPagamento",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "TipoViagem",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "ValorContratado",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "ValorDespesas",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "ValorPago",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "PagamentoParcial",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "ViagemId",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "DataHorarioSaida_Hora",
                table: "Viagens",
                newName: "HorarioSaida");

            migrationBuilder.RenameColumn(
                name: "DataHorarioSaidaGaragem_Hora",
                table: "Viagens",
                newName: "HorarioRetorno");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRetorno",
                table: "Viagens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataSaida",
                table: "Viagens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
