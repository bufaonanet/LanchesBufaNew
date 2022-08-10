using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesBufaNew.Migrations
{
    public partial class AddPedidoPedidoDetalhes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PEDIDOS",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Endereco1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Endereco2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PedidoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalItensPedido = table.Column<int>(type: "int", nullable: false),
                    PedidoEnviado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PedidoEntregueEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDOS", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_DETALHES",
                columns: table => new
                {
                    PedidoDetalheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    LancheId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_DETALHES", x => x.PedidoDetalheId);
                    table.ForeignKey(
                        name: "FK_PEDIDO_DETALHES_LANCHES_LancheId",
                        column: x => x.LancheId,
                        principalTable: "LANCHES",
                        principalColumn: "LancheId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEDIDO_DETALHES_PEDIDOS_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PEDIDOS",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_DETALHES_LancheId",
                table: "PEDIDO_DETALHES",
                column: "LancheId");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_DETALHES_PedidoId",
                table: "PEDIDO_DETALHES",
                column: "PedidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PEDIDO_DETALHES");

            migrationBuilder.DropTable(
                name: "PEDIDOS");
        }
    }
}
