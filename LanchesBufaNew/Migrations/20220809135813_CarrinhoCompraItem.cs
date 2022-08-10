using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesBufaNew.Migrations
{
    public partial class CarrinhoCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARRINHO_COMPRA_ITENS",
                columns: table => new
                {
                    CarrinhoCompraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LancheId = table.Column<int>(type: "int", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CarrinhoCompraId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARRINHO_COMPRA_ITENS", x => x.CarrinhoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CARRINHO_COMPRA_ITENS_LANCHES_LancheId",
                        column: x => x.LancheId,
                        principalTable: "LANCHES",
                        principalColumn: "LancheId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARRINHO_COMPRA_ITENS_LancheId",
                table: "CARRINHO_COMPRA_ITENS",
                column: "LancheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARRINHO_COMPRA_ITENS");
        }
    }
}
