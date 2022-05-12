namespace WebAPIAlmacen.Entidades
{
    public class UbicacionProducto
    {
        public int Id { get; set; }
        public int Pasillo { get; set; }
        public int Estanteria { get; set; }
        public int ProductoId { get; set; }
    }
}
