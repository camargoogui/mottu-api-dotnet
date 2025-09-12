using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    Ativo = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    FilialId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Filiais_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cpf",
                table: "Usuarios",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FilialId",
                table: "Usuarios",
                column: "FilialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
