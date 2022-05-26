using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIAlmacen.Entidades.Configuraciones
{
    public class FamiliaConfig : IEntityTypeConfiguration<Familia>
    {
        public void Configure(EntityTypeBuilder<Familia> builder)
        {
            
            builder.Property(x => x.Nombre).HasMaxLength(50).IsRequired();


            //builder.HasMany(x=>x.Productos).WithOne(x => x.Familia).HasForeignKey(x=> x.FamiliaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.Eliminado);   //Filtro puesto para que directamente no saque los eliminados.
        }
    }
}
