using System.ComponentModel.DataAnnotations;

namespace WebAPIAlmacen.Entidades
{
    public class Familia
    {
        public int Id { get; set; }
       // [Required]
       // [StringLength(50)] Ya especificado en la creacion de DbContext
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}
