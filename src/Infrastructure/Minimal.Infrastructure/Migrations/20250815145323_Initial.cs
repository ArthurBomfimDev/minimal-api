using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minimal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administradores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administradores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "veiculos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ano = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    data_criacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_administradores_email",
                table: "administradores",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_codigo",
                table: "veiculos",
                column: "codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administradores");

            migrationBuilder.DropTable(
                name: "veiculos");
        }
    }
}
