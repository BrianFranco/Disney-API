using Microsoft.EntityFrameworkCore.Migrations;

namespace Disney_API.Migrations
{
    public partial class QuintaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
