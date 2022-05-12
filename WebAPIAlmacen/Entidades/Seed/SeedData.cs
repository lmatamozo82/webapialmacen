using Microsoft.EntityFrameworkCore;

namespace WebAPIAlmacen.Entidades.Seed
{
    public class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Familias
            var tecnologia = new Familia { Id = 1, Nombre = "Tecnología" };
            var moda = new Familia { Id = 2, Nombre = "Moda" };
            var hogar = new Familia { Id = 3, Nombre = "Hogar" };

            modelBuilder.Entity<Familia>().HasData(tecnologia, moda, hogar);

            //Productos
            var portatil = new Producto() { Id = 1, Nombre = "Portátil", Precio = 1000, FechaAlta = new DateTime(2020, 1, 1), Descatalogado = false, FotoURL = null, FamiliaId = tecnologia.Id };
            var impresora = new Producto() { Id = 2, Nombre = "Impresora", Precio = 100, FechaAlta = new DateTime(2020, 1, 30), Descatalogado = false, FotoURL = null, FamiliaId = tecnologia.Id };
            var ibm = new Producto() { Id = 3, Nombre = "IBM", Precio = 1800, FechaAlta = new DateTime(2000, 1, 1), Descatalogado = true, FotoURL = null, FamiliaId = tecnologia.Id };

            var camisa = new Producto() { Id = 4, Nombre = "Camisa", Precio = 10, FechaAlta = new DateTime(2021, 5, 10), Descatalogado = false, FotoURL = null, FamiliaId = moda.Id };
            var pantalon = new Producto() { Id = 5, Nombre = "Pantalón", Precio = 20, FechaAlta = new DateTime(2020, 3, 30), Descatalogado = false, FotoURL = null, FamiliaId = moda.Id };
            var traje = new Producto() { Id = 6, Nombre = "Traje", Precio = 150, FechaAlta = new DateTime(2010, 8, 1), Descatalogado = true, FotoURL = null, FamiliaId = moda.Id };
            var lavadora = new Producto() { Id = 7, Nombre = "Lavadora", Precio = 800, FechaAlta = new DateTime(2018, 5, 11), Descatalogado = false, FotoURL = null, FamiliaId = hogar.Id };
            var microondas = new Producto() { Id = 8, Nombre = "Microondas", Precio = 120, FechaAlta = new DateTime(2017, 4, 3), Descatalogado = false, FotoURL = null, FamiliaId = hogar.Id };
            var cafetera = new Producto() { Id = 9, Nombre = "Cafetera", Precio = 150, FechaAlta = new DateTime(2008, 9, 1), Descatalogado = true, FotoURL = null, FamiliaId = hogar.Id };

            modelBuilder.Entity<Producto>().HasData(portatil, impresora, ibm, camisa, pantalon, traje, lavadora, microondas, cafetera);

            //UbicacionesProductos
            var ubicacion1 = new UbicacionProducto() { Id = 1, Pasillo = 1, Estanteria = 1, ProductoId = portatil.Id };
            var ubicacion2 = new UbicacionProducto() { Id = 2, Pasillo = 1, Estanteria = 2, ProductoId = impresora.Id };
            var ubicacion3 = new UbicacionProducto() { Id = 3, Pasillo = 1, Estanteria = 3, ProductoId = ibm.Id };
            var ubicacion4 = new UbicacionProducto() { Id = 4, Pasillo = 2, Estanteria = 1, ProductoId = camisa.Id };
            var ubicacion5 = new UbicacionProducto() { Id = 5, Pasillo = 2, Estanteria = 2, ProductoId = pantalon.Id };
            var ubicacion6 = new UbicacionProducto() { Id = 6, Pasillo = 2, Estanteria = 3, ProductoId = traje.Id };
            var ubicacion7 = new UbicacionProducto() { Id = 7, Pasillo = 3, Estanteria = 1, ProductoId = lavadora.Id };
            var ubicacion8 = new UbicacionProducto() { Id = 8, Pasillo = 3, Estanteria = 2, ProductoId = microondas.Id };
            var ubicacion9 = new UbicacionProducto() { Id = 9, Pasillo = 3, Estanteria = 3, ProductoId = cafetera.Id };

            modelBuilder.Entity<UbicacionProducto>().HasData(ubicacion1, ubicacion2, ubicacion3, ubicacion4, ubicacion5, ubicacion6, ubicacion7, ubicacion8, ubicacion9);

            //Distribuidores
            var distribuidor1 = new Distribuidor { Id = 1, Nombre = "Distribuidor 1" };
            var distribuidor2 = new Distribuidor { Id = 2, Nombre = "Distribuidor 2" };

            modelBuilder.Entity<Distribuidor>().HasData(distribuidor1, distribuidor2);

            //DistribuidoresProductos
            var distribucion1 = new DistribuidorProducto() { DistribuidorId = distribuidor1.Id, ProductoId = portatil.Id };
            var distribucion2 = new DistribuidorProducto() { DistribuidorId = distribuidor1.Id, ProductoId = impresora.Id };
            var distribucion3 = new DistribuidorProducto() { DistribuidorId = distribuidor1.Id, ProductoId = camisa.Id };
            var distribucion4 = new DistribuidorProducto() { DistribuidorId = distribuidor1.Id, ProductoId = lavadora.Id };
            var distribucion5 = new DistribuidorProducto() { DistribuidorId = distribuidor1.Id, ProductoId = cafetera.Id };
            var distribucion6 = new DistribuidorProducto() { DistribuidorId = distribuidor2.Id, ProductoId = portatil.Id };
            var distribucion7 = new DistribuidorProducto() { DistribuidorId = distribuidor2.Id, ProductoId = impresora.Id };
            var distribucion8 = new DistribuidorProducto() { DistribuidorId = distribuidor2.Id, ProductoId = ibm.Id };
            var distribucion9 = new DistribuidorProducto() { DistribuidorId = distribuidor2.Id, ProductoId = pantalon.Id };
            var distribucion10 = new DistribuidorProducto() { DistribuidorId = distribuidor2.Id, ProductoId = traje.Id };
            var distribucion11 = new DistribuidorProducto() { DistribuidorId = distribuidor2.Id, ProductoId = lavadora.Id };

            modelBuilder.Entity<DistribuidorProducto>().HasData(distribucion1, distribucion2, distribucion3, distribucion4, distribucion5, distribucion6,
                distribucion7, distribucion8, distribucion9, distribucion10, distribucion11);

        }

    }
}
