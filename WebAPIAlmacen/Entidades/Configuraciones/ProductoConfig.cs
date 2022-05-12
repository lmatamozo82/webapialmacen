using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIAlmacen.Entidades.Configuraciones
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>

    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
            builder.Property(x => x.FechaAlta).HasColumnType("date");
            builder.Property(x => x.Precio).HasPrecision(precision: 9, scale: 2);
        }
    }
}
