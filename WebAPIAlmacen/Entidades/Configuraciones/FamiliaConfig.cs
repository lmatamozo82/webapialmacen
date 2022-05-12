using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIAlmacen.Entidades.Configuraciones
{
    public class FamiliaConfig : IEntityTypeConfiguration<Familia>
    {
        public void Configure(EntityTypeBuilder<Familia> builder)
        {
            
            builder.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
        }
    }
}
