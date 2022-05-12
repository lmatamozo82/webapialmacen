using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIAlmacen.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Distribuidores",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Distribuidor 1" },
                    { 2, "Distribuidor 2" }
                });

            migrationBuilder.InsertData(
                table: "Familias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Tecnología" },
                    { 2, "Moda" },
                    { 3, "Hogar" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Descatalogado", "FamiliaId", "FechaAlta", "FotoURL", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, false, 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Portátil", 1000m },
                    { 2, false, 1, new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Impresora", 100m },
                    { 3, true, 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IBM", 1800m },
                    { 4, false, 2, new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Camisa", 10m },
                    { 5, false, 2, new DateTime(2020, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pantalón", 20m },
                    { 6, true, 2, new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Traje", 150m },
                    { 7, false, 3, new DateTime(2018, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lavadora", 800m },
                    { 8, false, 3, new DateTime(2017, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Microondas", 120m },
                    { 9, true, 3, new DateTime(2008, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cafetera", 150m }
                });

            migrationBuilder.InsertData(
                table: "DistribuidoresProductos",
                columns: new[] { "DistribuidorId", "ProductoId", "UnidadesVendidas" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 1, 2, 0 },
                    { 1, 4, 0 },
                    { 1, 7, 0 },
                    { 1, 9, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 0 },
                    { 2, 3, 0 },
                    { 2, 5, 0 },
                    { 2, 6, 0 },
                    { 2, 7, 0 }
                });

            migrationBuilder.InsertData(
                table: "UbicacionesProductos",
                columns: new[] { "Id", "Estanteria", "Pasillo", "ProductoId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 3, 1, 3 },
                    { 4, 1, 2, 4 },
                    { 5, 2, 2, 5 },
                    { 6, 3, 2, 6 },
                    { 7, 1, 3, 7 },
                    { 8, 2, 3, 8 },
                    { 9, 3, 3, 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "DistribuidoresProductos",
                keyColumns: new[] { "DistribuidorId", "ProductoId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UbicacionesProductos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Distribuidores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Distribuidores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Familias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Familias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Familias",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
