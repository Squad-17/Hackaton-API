using Microsoft.EntityFrameworkCore.Migrations;

namespace Hackaton_API.Migrations
{
    public partial class CargoFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "Funcionarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Funcionarios");
        }
    }
}
