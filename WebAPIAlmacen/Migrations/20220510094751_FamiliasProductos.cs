using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIAlmacen.Migrations
{
    public partial class FamiliasProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamiliaId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FamiliaId",
                table: "Productos",
                column: "FamiliaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Familias_FamiliaId",
                table: "Productos",
                column: "FamiliaId",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Familias_FamiliaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_FamiliaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "FamiliaId",
                table: "Productos");
        }
    }
}
