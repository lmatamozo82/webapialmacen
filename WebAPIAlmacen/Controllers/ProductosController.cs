using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.DTOs;
using WebAPIAlmacen.Entidades;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController:ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IHttpClientFactory clientFactory;

        public ProductosController(ApplicationDBContext context,IHttpClientFactory clientFactory)
        {
            this.context=context;
            this.clientFactory = clientFactory;

        }

        [HttpGet("productosagrupadosdescatalogado")]
        public async Task<ActionResult> GetAgrupadoDescatalogado()
        {
            var productos = await context.Productos.GroupBy(x => x.Descatalogado)
                .Select(x => new
                {
                    Descatalogado = x.Key,
                    Total = x.Count(),
                    Productos = x.ToList()
                }).ToListAsync();

            return Ok(productos);
        }


        [HttpGet("filtro")]
        public async Task<ActionResult> GetProductosFiltro([FromQuery] DTOProductosFiltro filtroProductos)
        {
            var productosQueryable = context.Productos.AsQueryable(); //Hacemos esto para que no se ejecute hasta que no hagamos todo lo necesario.

            if (!string.IsNullOrEmpty(filtroProductos.Nombre))
            {
                productosQueryable = productosQueryable.Where(x => x.Nombre.Contains(filtroProductos.Nombre));
            }

            productosQueryable = productosQueryable.Where(x => x.Descatalogado==filtroProductos.Descatalogado);

            productosQueryable = productosQueryable.Where(x => x.Id == filtroProductos.FamiliaId);

            var productos= await productosQueryable.ToListAsync(); //La SQL se ejecuta aqui, no antes.

            return Ok(productos);


            //Mal hecho:
            //var prods = await context.Productos.ToListAsync();
            //prods.where();

        }

        [HttpGet("keyless")]
        public async Task<ActionResult> GetProductoKeyless()
        {
            var productos = await context.ProductoKeyLess.ToListAsync();

            return Ok(productos);
        }

        [HttpGet("familias")]
        public async Task<ActionResult> GetFamiliasHttpClientFactory()
        {
            var cliente = clientFactory.CreateClient("webapi");
            var response = await cliente.GetFromJsonAsync<List<Familia>>("/api/familias");

            //List<Familia> familias = new List<Familia>();

            //if (response.IsSuccessStatusCode)
            //{
            //    var contentStream = await response.Content.ReadAsStringAsync();
            //    JArray fams = JArray.Parse(contentStream) as JArray;
            //    foreach (var f in fams)
            //    {
            //        Familia fam = JsonConvert.DeserializeObject<Familia>(f.ToString());
            //        familias.Add(fam);
            //    }
            //}

            return Ok(response);
         }
    }

}
