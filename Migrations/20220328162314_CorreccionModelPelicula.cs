using Microsoft.EntityFrameworkCore.Migrations;

namespace Disney_API.Migrations
{
    public partial class CorreccionModelPelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelicula_Genero_GeneroId",
                table: "Pelicula");

            migrationBuilder.AlterColumn<int>(
                name: "GeneroId",
                table: "Pelicula",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelicula_Genero_GeneroId",
                table: "Pelicula",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelicula_Genero_GeneroId",
                table: "Pelicula");

            migrationBuilder.AlterColumn<int>(
                name: "GeneroId",
                table: "Pelicula",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelicula_Genero_GeneroId",
                table: "Pelicula",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
