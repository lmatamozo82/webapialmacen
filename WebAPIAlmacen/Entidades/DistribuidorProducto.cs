namespace WebAPIAlmacen.Entidades
{
    public class DistribuidorProducto
    {
        public int DistribuidorId { get; set; }
        public int ProductoId { get; set; }
        public int UnidadesVendidas { get; set; }
        public Distribuidor Distribuidor { get; set; }
        public Producto Producto { get; set; }
    }
}
