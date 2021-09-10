using Microsoft.EntityFrameworkCore.Migrations;

namespace Hackaton_API.Migrations
{
    public partial class AvatarFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "avatar",
                table: "Funcionarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                table: "Funcionarios");
        }
    }
}
