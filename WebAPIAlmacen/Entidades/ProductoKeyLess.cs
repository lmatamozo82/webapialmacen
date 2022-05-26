using Microsoft.EntityFrameworkCore;

namespace WebAPIAlmacen.Entidades
{
    [Keyless]
    public class ProductoKeyLess
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public decimal Precio { get; set; }
    }
}
