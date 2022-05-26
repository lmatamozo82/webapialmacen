namespace WebAPIAlmacen.DTOs
{
    public class DTOFamiliaProductos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TotalProductos { get; set; }
        public List<DTOProductoItem> Productos { get; set; }

    }
}
