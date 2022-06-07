using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIAlmacen.Migrations
{
    public partial class ProcedimientosAlmacenados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"CREATE PROCEDURE Familias_ObtenerPorId
                @id int
                AS
                BEGIN
                SET NOCOUNT ON;
                SELECT *
                FROM Familias
                WHERE Id = @id;
                END");

            migrationBuilder.Sql(@"CREATE PROCEDURE Familias_Insertar
                @nombre nvarchar(150),
                @id int OUTPUT
                AS
                BEGIN
                SET NOCOUNT ON;
                INSERT INTO Familias(Nombre)
                VALUES (@nombre);
                SELECT @id = SCOPE_IDENTITY();
                END");
            



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[Familias_ObtenerPorId]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[Familias_Insertar]");
        }
    }
}
