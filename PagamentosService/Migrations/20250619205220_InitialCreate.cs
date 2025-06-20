using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PagamentosService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TreinoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    FormaPagamento = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TransacaoId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    NomeAluno = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EmailAluno = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    NomeTreino = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcessosTreino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TreinoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PagamentoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataLiberacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessosTreino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcessosTreino_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcessosTreino_AlunoId_TreinoId_PagamentoId",
                table: "AcessosTreino",
                columns: new[] { "AlunoId", "TreinoId", "PagamentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcessosTreino_PagamentoId",
                table: "AcessosTreino",
                column: "PagamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcessosTreino");

            migrationBuilder.DropTable(
                name: "Pagamentos");
        }
    }
}
