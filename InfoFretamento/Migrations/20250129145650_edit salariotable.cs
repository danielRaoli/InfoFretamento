using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoFretamento.Migrations
{
    /// <inheritdoc />
    public partial class editsalariotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salario_Pessoa_Id",
                table: "Salario");

            migrationBuilder.DropForeignKey(
                name: "FK_Salario_Pessoa_Id1",
                table: "Salario");

            migrationBuilder.DropForeignKey(
                name: "FK_Salario_Pessoa_ResponsavelId",
                table: "Salario");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Salario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Salario_Pessoa_ResponsavelId",
                table: "Salario",
                column: "ResponsavelId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salario_Pessoa_ResponsavelId",
                table: "Salario");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Salario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Salario_Pessoa_Id",
                table: "Salario",
                column: "Id",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salario_Pessoa_Id1",
                table: "Salario",
                column: "Id",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salario_Pessoa_ResponsavelId",
                table: "Salario",
                column: "ResponsavelId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
