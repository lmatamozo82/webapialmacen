using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPIAlmacen.DTOs;
using WebAPIAlmacen.Entidades;
using WebAPIAlmacen.Servicios;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IHttpClientFactory clientFactory;
        private readonly IGestorArchivos gestorArchivosLocal;

        public ProductosController(ApplicationDBContext context, IHttpClientFactory clientFactory, IGestorArchivos gestorArchivosLocal)
        {
            this.context = context;
            this.clientFactory = clientFactory;
            this.gestorArchivosLocal = gestorArchivosLocal;
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

            productosQueryable = productosQueryable.Where(x => x.Descatalogado == filtroProductos.Descatalogado);

            productosQueryable = productosQueryable.Where(x => x.Id == filtroProductos.FamiliaId);

            var productos = await productosQueryable.ToListAsync(); //La SQL se ejecuta aqui, no antes.

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


        // EF requiere que una consulta SQL devuelva un tipo determinado. ESte ejemplo con ADO Net lo resolvería
        [HttpGet("sqlcommand2")]
        public async Task<ActionResult<IEnumerable<ProductoKeyLess>>> GetProductosSqlCommand2()
        {
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT Familias.Id, Familias.Nombre, Productos.Id,Productos.Nombre, Productos.Precio FROM Familias INNER JOIN Productos ON Familias.Id = Productos.FamiliaId";
            command.CommandType = CommandType.Text;

            context.Database.OpenConnection();
            var productos = new List<object>();

            using (var result = await command.ExecuteReaderAsync())
            {
                while (result.Read())
                {
                    var producto = new
                    {
                        IdFamilia = result.GetValue(0),
                        NombreFamilia = result.GetValue(1),
                        IdProducto = result.GetValue(2),
                        NombreProducto = result.GetValue(3),
                        Precio = result.GetValue(4)
                    };
                    productos.Add(producto);
                }
            }

            return Ok(productos);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] DTOProductoAgregar producto)
        {
            Producto newProducto = new Producto
            {
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Descatalogado = false,
                FechaAlta = DateTime.Now,
                FamiliaId = producto.FamiliaId
            };


            if (producto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await producto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(producto.Foto.FileName);
                    newProducto.FotoURL = await gestorArchivosLocal.GuardarArchivo(contenido, extension, "imagenes",
                        producto.Foto.ContentType);
                }
            }

            await context.AddAsync(newProducto);
            await context.SaveChangesAsync();
            return Ok(newProducto);
        }


    }


}
