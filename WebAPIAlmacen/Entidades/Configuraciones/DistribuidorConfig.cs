using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIAlmacen.Entidades.Configuraciones
{
    public class DistribuidorConfig : IEntityTypeConfiguration<Distribuidor>
    {

        public void Configure(EntityTypeBuilder<Distribuidor> builder)
        {
            builder.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
        }
    }
}
