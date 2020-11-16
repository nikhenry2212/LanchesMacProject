using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanchesMac.Migrations
{
    public partial class Pedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cep = table.Column<string>(maxLength: 10, nullable: false),
                    Cidade = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 80, nullable: false),
                    Endereco1 = table.Column<string>(maxLength: 100, nullable: false),
                    Endereco2 = table.Column<string>(maxLength: 100, nullable: false),
                    Estado = table.Column<string>(maxLength: 10, nullable: true),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    PedidoEnviado = table.Column<DateTime>(nullable: false),
                    PedidoTotal = table.Column<decimal>(nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDetalhes",
                columns: table => new
                {
                    PedidoDetalheId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LancheId = table.Column<int>(nullable: false),
                    PedidoId = table.Column<int>(nullable: false),
                    Preco = table.Column<decimal>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDetalhes", x => x.PedidoDetalheId);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhes_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "LancheId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhes_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhes_LancheId",
                table: "PedidosDetalhes",
                column: "LancheId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhes_PedidoId",
                table: "PedidosDetalhes",
                column: "PedidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosDetalhes");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
