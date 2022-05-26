using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIAlmacen.Migrations
{
    public partial class familiaseliminado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Familias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Familias");
        }
    }
}
