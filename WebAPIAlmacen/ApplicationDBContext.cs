using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPIAlmacen.Entidades;
using WebAPIAlmacen.Entidades.Configuraciones;
using WebAPIAlmacen.Entidades.Seed;

namespace WebAPIAlmacen
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder) //Sirve para poder personalizar el metodo de creación, asi lo personalizamos y validamos
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Familia>().HasKey(x => x.Id); //Le decimos que es clave principal.
            //modelBuilder.Entity<Familia>().Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Producto>().Property(x => x.Nombre).HasMaxLength(150).IsRequired();
            //modelBuilder.Entity<Producto>().Property(x => x.FechaAlta).HasColumnType("date");
            //modelBuilder.Entity<Producto>().Property(x => x.Precio).HasPrecision(precision: 9, scale: 2);

            //modelBuilder.Entity<Distribuidor>().Property(x => x.Nombre).HasMaxLength(150).IsRequired();
            //modelBuilder.Entity<DistribuidorProducto>().HasKey(x => new { x.DistribuidorId, x.ProductoId });

            //Opción 1. Añadir todas las clases de configuracion
            //modelBuilder.ApplyConfiguration(new FamiliaConfig());

            //Opción 2. Clase que escanea todas las configuraciones que hereden de la clase de configuracion (implementa el interfaz) y las añade.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductoKeyLess>().HasNoKey().ToSqlQuery("SELECT Id, Nombre, Precio from Productos").ToView(null);


            SeedData.Seed(modelBuilder);
        }


        public DbSet<Familia> Familias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<UbicacionProducto> UbicacionesProductos { get; set; }
        public DbSet<Distribuidor> Distribuidores { get; set; }
        public DbSet<DistribuidorProducto> DistribuidoresProductos { get; set; }
        public DbSet<ProductoKeyLess> ProductoKeyLess { get; set; } 

    }
}
