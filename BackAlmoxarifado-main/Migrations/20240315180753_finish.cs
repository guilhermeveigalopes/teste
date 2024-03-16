using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxarifadoAPI.Migrations
{
    public partial class finish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    idEmail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailUsuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.idEmail);
                });

            migrationBuilder.CreateTable(
                name: "GestaoProdutos",
                columns: table => new
                {
                    idProduto = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    preco = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    estoqueAtual = table.Column<int>(type: "int", nullable: true),
                    estoqueMinimo = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GestaoPr__5EEDF7C3DE3C8957", x => x.idProduto);
                });

            migrationBuilder.CreateTable(
                name: "LOGROBO",
                columns: table => new
                {
                    iDlOG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoRobo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioRobo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLog = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etapa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformacaoLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProdutoAPI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGROBO", x => x.iDlOG);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "GestaoProdutos");

            migrationBuilder.DropTable(
                name: "LOGROBO");
        }
    }
}
