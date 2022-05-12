namespace WebAPIAlmacen.Entidades
{
    public class Distribuidor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public ICollection<DistribuidorProducto> DistribuidorProductos { get; set; }
    }
}
