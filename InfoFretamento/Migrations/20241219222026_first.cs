using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Vencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoDocumento = table.Column<string>(type: "text", nullable: false),
                    Referencia = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Documento_Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Documento_Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Endereco_Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Endereco_Rua = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Endereco_Bairro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Endereco_Numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Cpf = table.Column<string>(type: "character(14)", fixedLength: true, maxLength: 14, nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Cliente_Tipo = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Habilitacao_Protocolo = table.Column<string>(type: "text", nullable: true),
                    Habilitacao_Vencimento = table.Column<DateOnly>(type: "date", nullable: true),
                    Habilitacao_Categoria = table.Column<string>(type: "text", nullable: true),
                    Habilitacao_Cidade = table.Column<string>(type: "text", nullable: true),
                    Habilitacao_Uf = table.Column<string>(type: "text", nullable: true),
                    Cartao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Matricula = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Prefixo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    KmAtual = table.Column<int>(type: "integer", nullable: false),
                    Placa = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Marca = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LocalEmplacado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Uf = table.Column<string>(type: "text", nullable: false),
                    Carroceria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CapacidadeTank = table.Column<int>(type: "integer", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    QuantidadePoltronas = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Modelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Viagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rota_Saida_UfSaida = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Rota_Saida_CidadeSaida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rota_Saida_LocalDeSaida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rota_Retorno_UfSaida = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Rota_Retorno_CidadeSaida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rota_Retorno_LocalDeSaida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataHorarioSaida_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    DataHorarioSaida_Hora = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    DataHorarioRetorno_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    DataHorarioRetorno_Hora = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    DataHorarioSaidaGaragem_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    DataHorarioSaidaGaragem_Hora = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    DataHorarioChegada_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    DataHorarioChegada_Hora = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    TipoServico = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TipoViagem = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MotoristaId = table.Column<int>(type: "integer", nullable: false),
                    VeiculoId = table.Column<int>(type: "integer", nullable: false),
                    TipoPagamento = table.Column<string>(type: "text", nullable: false),
                    Parcelas = table.Column<int>(type: "integer", nullable: false),
                    ValorContratado = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorDespesas = table.Column<decimal>(type: "numeric", nullable: false),
                    Itinerario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viagens_Pessoa_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viagens_Pessoa_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viagens_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViagensProgramadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Saida_Local = table.Column<string>(type: "text", nullable: false),
                    Saida_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Saida_Hora = table.Column<string>(type: "text", nullable: false),
                    Retorno_Local = table.Column<string>(type: "text", nullable: false),
                    Retorno_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Retorno_Hora = table.Column<string>(type: "text", nullable: false),
                    Chegada_Local = table.Column<string>(type: "text", nullable: false),
                    Chegada_Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Chegada_Hora = table.Column<string>(type: "text", nullable: false),
                    ValorPassagem = table.Column<decimal>(type: "numeric", nullable: false),
                    FormaPagto = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Responsavel = table.Column<string>(type: "text", nullable: false),
                    Guia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Itinerario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Observacoes = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VeiculoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViagensProgramadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViagensProgramadas_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataEmissao = table.Column<DateTime>(type: "date", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "date", nullable: false),
                    OrigemPagamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ResponsavelId = table.Column<int>(type: "integer", nullable: false),
                    ViagemId = table.Column<int>(type: "integer", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "date", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorParcial = table.Column<decimal>(type: "numeric", nullable: false),
                    FormaPagamento = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CentroCusto = table.Column<string>(type: "text", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    MotoristaId = table.Column<int>(type: "integer", nullable: true),
                    VeiculoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Pessoa_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Despesas_Pessoa_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Despesas_Pessoa_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Despesas_Pessoa_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despesas_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Despesas_Viagens_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataEmissao = table.Column<DateTime>(type: "date", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "date", nullable: false),
                    OrigemPagamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ResponsavelId = table.Column<int>(type: "integer", nullable: false),
                    ViagemId = table.Column<int>(type: "integer", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "date", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorParcial = table.Column<decimal>(type: "numeric", nullable: false),
                    FormaPagamento = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CentroCusto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receitas_Pessoa_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receitas_Viagens_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ViagemId = table.Column<int>(type: "integer", nullable: false),
                    PassageiroId = table.Column<int>(type: "integer", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "date", nullable: false),
                    FormaPagamento = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Poltrona = table.Column<int>(type: "integer", nullable: false),
                    Situacao = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passagens_Pessoa_PassageiroId",
                        column: x => x.PassageiroId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passagens_ViagensProgramadas_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "ViagensProgramadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_ResponsavelId",
                table: "Despesas",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_VeiculoId",
                table: "Despesas",
                column: "VeiculoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Passagens_PassageiroId",
                table: "Passagens",
                column: "PassageiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Passagens_ViagemId",
                table: "Passagens",
                column: "ViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_ResponsavelId",
                table: "Receitas",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_ViagemId",
                table: "Receitas",
                column: "ViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Viagens_ClienteId",
                table: "Viagens",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Viagens_MotoristaId",
                table: "Viagens",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_Viagens_VeiculoId",
                table: "Viagens",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_ViagensProgramadas_VeiculoId",
                table: "ViagensProgramadas",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Manutencoes");

            migrationBuilder.DropTable(
                name: "Passagens");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "ViagensProgramadas");

            migrationBuilder.DropTable(
                name: "Viagens");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
