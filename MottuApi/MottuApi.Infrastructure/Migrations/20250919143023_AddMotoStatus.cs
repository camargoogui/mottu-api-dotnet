using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMotoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "Motos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Motos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Motos");

            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "Motos",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
