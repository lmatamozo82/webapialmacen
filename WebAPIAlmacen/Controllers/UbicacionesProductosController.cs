using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.Entidades;
using WebAPIAlmacen.Helpers;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/ubicacionesproductos")]
    [ServiceFilter(typeof(ProductoExiste))]
    public class UbicacionesProductosController : ControllerBase
    {

        private readonly ApplicationDBContext context;

        public UbicacionesProductosController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<UbicacionProducto>> GetUbicaciones()
        {
            return await context.UbicacionesProductos.ToListAsync();
        }

        [HttpGet("producto/{productoId}")]
        public async Task<UbicacionProducto> GetUbicacionProducto(int productoId)
        {
            return await context.UbicacionesProductos.FirstOrDefaultAsync(x => x.ProductoId == productoId);
        }
    }

}
