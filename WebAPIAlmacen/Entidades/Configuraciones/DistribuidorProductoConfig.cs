using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIAlmacen.Entidades.Configuraciones
{
    public class DistribuidorProductoConfig : IEntityTypeConfiguration<DistribuidorProducto>
    {
        public void Configure(EntityTypeBuilder<DistribuidorProducto> builder)
        {
            builder.HasKey(x => new { x.DistribuidorId, x.ProductoId });
        }
    }
}
