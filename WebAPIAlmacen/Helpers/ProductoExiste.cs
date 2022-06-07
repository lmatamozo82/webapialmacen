using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace WebAPIAlmacen.Helpers
{
    public class ProductoExiste : Attribute, IAsyncResourceFilter

    {
        private readonly ApplicationDBContext dbcontext;

        public ProductoExiste(ApplicationDBContext context)
        {
            this.dbcontext = context;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context,
            ResourceExecutionDelegate next)
        {
            var productoIdObject = context.HttpContext.Request.RouteValues["productoId"];

            if (productoIdObject == null)
            {
                await next(); // Sigue hacia adelante
                return; // Detiene la revisión del filtro
            }


            var productoId = int.Parse(productoIdObject.ToString());

            var existeProducto = await dbcontext.Productos.AnyAsync(x => x.Id == productoId);

            if (!existeProducto)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }

    }
}
