using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.DTOs;
using WebAPIAlmacen.Entidades;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/familias")]
    public class FamiliasController :ControllerBase
    {
        private readonly ApplicationDBContext context;

        public FamiliasController(ApplicationDBContext context)
        {
            this.context = context;
        }

        //[HttpGet("listadofamilias")]
        //[HttpGet("/listadofamilias")]
        [HttpGet]
        public async Task<IEnumerable<Familia>> Get()
        {
            return await context.Familias.ToListAsync();
        }

        [HttpGet("sync")]   //Este tipo de petición sincrona, no libera el hilo de ejecución y no puede atender otras peticiones en el mismo hilo. NO USAR
        public  IEnumerable<Familia> GetSync()
        {
            return context.Familias.ToList();
        }

        [HttpGet("notraking")]
        public async Task<IEnumerable<Familia>> GetFamiliasNoTracking()
        {
            return await context.Familias.AsNoTracking().ToListAsync(); //Innecesario puesto que ya lo hemos puesto en Program.cs para el DBContext.
        }

        //[HttpGet("{id}")]   
        [HttpGet("sql/{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliaPorIdSQL(int id)
        {
            //Ejemplo de uso de SQLs a modo tradicional.   
            //var familia = await context.Familias.FromSqlInterpolated($"SELECT * FROM Familias WHERE Id={id}").FirstOrDefaultAsync();

            var familia = await context.Familias.FromSqlRaw("SELECT * FROM Familias WHERE Id={0}",id).FirstOrDefaultAsync();


            return Ok(familia);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliaPorId(int id)
        {
            //var familia=await context.Familias.SingleOrDefaultAsync();
            //var familia = await context.Familias.FirstOrDefaultAsync(x => x.Id == id);

            var familia = await context.Familias.FindAsync(id);  //OJO FindAsync solo funciona para campos clave.

            return Ok(familia);
        }



        [HttpGet("{contiene}")]
        public async Task<ActionResult<Familia>> GetFamiliaContiene(string contiene)
        {
            var familia = await context.Familias.FirstOrDefaultAsync(x=>x.Nombre.Contains(contiene));

            //var familia = await context.Familias.Where(x => x.Nombre.Contains(contiene)).FirstOrDefaultAsync()); esto tb vale

            return Ok(familia);
        }

        [HttpGet("iactionresult/{contiene}")] //[FromRoute] argumentos va por la ruta. Es el comportamiento por defecto
        public async Task<IActionResult> GetFamiliaContieneIActionResult(string contiene)
        {
            var familia = await context.Familias.FirstOrDefaultAsync(x => x.Nombre.Contains(contiene));

            //var familia = await context.Familias.Where(x => x.Nombre.Contains(contiene)).FirstOrDefaultAsync()); esto tb vale

            return Ok(familia);
        }


        [HttpGet("contieneparamquerystring")]
        public async Task<ActionResult<Familia>> GetFamiliaContieneQueryString([FromQuery]string contiene)
        {
            var familia = await context.Familias.FirstOrDefaultAsync(x => x.Nombre.Contains(contiene));

            //var familia = await context.Familias.Where(x => x.Nombre.Contains(contiene)).FirstOrDefaultAsync()); esto tb vale

            return Ok(familia);
        }

        [HttpGet("contieneparamheader")]
        public async Task<ActionResult<Familia>> GetFamiliaContieneHeader([FromHeader] string contiene)
        {
            var familia = await context.Familias.FirstOrDefaultAsync(x => x.Nombre.Contains(contiene));

            if (familia==null)
             {
                return NotFound();
             }

            //var familia = await context.Familias.Where(x => x.Nombre.Contains(contiene)).FirstOrDefaultAsync()); esto tb vale

            return Ok(familia);
        }


        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasPaginacion()
        {
            //var familias = await context.Familias.Take(2).ToListAsync();
            var familias = await context.Familias.Skip(1).Take(2).ToListAsync();

            return Ok(familias);
        }

        [HttpGet("paginacion2")]
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasPaginacion2(int pagina=1)
        {
            //var familias = await context.Familias.Take(2).ToListAsync();
            int registrosporpagina = 2;
            var familias = await context.Familias.Skip((pagina-1) * registrosporpagina)
                .Take(registrosporpagina).ToListAsync();

            return Ok(familias);
        }

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasSelect()
        {
            
            var familias = await context.Familias.Select(x=> new {Id=x.Id,Nombre=x.Nombre }).ToListAsync();

            //Tb puedo hacer esto.
            //var familia2 = await (from x in context.Familias
            //                      select new
            //                      {
            //                          Id = x.Id,
            //                          Nombre = x.Nombre
            //                      }).ToListAsync();
            

            return Ok(familias);
        }

        [HttpGet("FamiliasProductos/{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliaProductos(int id)
        {

            var familias = await context.Familias.Include(x=>x.Productos).FirstOrDefaultAsync(x=>x.Id==id);

            return Ok(familias);
        }

        [HttpGet("FamiliasProductosThenInclude/{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliaProductosThenInclude(int id)
        {

            var familias = await context.Familias
                .Include(x => x.Productos.OrderBy(x=>x.Nombre))
                .ThenInclude(x=>x.DistribuidorProductos)
                .ThenInclude(x=>x.Distribuidor)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(familias);
        }

        [HttpGet("FamiliasProductosSelect/{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliaProductosSelect(int id)
        {

            var familias = await (from x in context.Familias
                                  where x.Id==id
                                  select new DTOFamiliaProductos
                                  {
                                      Id = x.Id,
                                      Nombre = x.Nombre,
                                      TotalProductos = x.Productos.Count,
                                      Productos = x.Productos.Select(y => new DTOProductoItem
                                      {
                                          IdProducto = y.Id,
                                          Nombre = y.Nombre
                                      }).ToList()
                                  }).FirstOrDefaultAsync();


            return Ok(familias);
        }



        [HttpPost]
        public async Task<ActionResult<Familia>> Post(Familia familia)
        {

            var estatus = context.Entry(familia).State; //Detached. Sin seguimiento (viene de fuera y no se conoce).

            await context.AddAsync(familia);
            var estatus2 = context.Entry(familia).State;  //Added. Agregado y conocido.

            await context.SaveChangesAsync();
            var estatus3 = context.Entry(familia).State; //Unchanged. Cambiado y sin modificar.

            return Ok();


        }

        [HttpPost]
        public async Task<ActionResult<Familia>> PostVarios(Familia[] familias)
        {
            await context.AddRangeAsync(familias);
            await context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("familiaproductos")]
        public async Task<ActionResult> PostFamiliaProductos(Familia familia)
        {
            await context.AddAsync(familia);
            await context.SaveChangesAsync();
            return Created("Familia", new {Familia = familia});
                        
        }

        [HttpPost("familiaproductosdto")]
        public async Task<ActionResult> PostFamiliaProductosDTO(DTOFamiliaProductos familia)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var newFamilia = new Familia()
                {
                    Nombre = familia.Nombre
                };

                await context.AddAsync(newFamilia);
                await context.SaveChangesAsync();

                foreach (var producto in familia.Productos)
                {
                    var newProducto = new Producto()
                    {
                        Nombre = producto.Nombre,
                        FechaAlta = DateTime.Now,
                        FamiliaId = newFamilia.Id,
                        Descatalogado = false
                    };

                    await context.AddAsync(newProducto);
                }
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Created("Familia", new { Familia = newFamilia });
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest("De ha producido un error.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutFamilias(int id, [FromBody] Familia familia)
        {
            if (id != familia.Id)
            {
                return BadRequest("Los ids no coinciden");
            }

            var existe = await context.Familias.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(familia);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("putTraking/{id:int}")]
        public async Task<ActionResult> PutFamiliasTracking(int id, [FromBody] Familia familia)
        {
            if (id != familia.Id)
            {
                return BadRequest("Los ids no coinciden");
            }
            
            //Tracking activado en la consulta pq queremos hacer un seguimiento pq lo modificamos.
            var familiaUpdate = await context.Familias.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (familiaUpdate == null)
            {
                return NotFound();
            }

            familiaUpdate.Nombre = familia.Nombre;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteFamilias(int id)
        {

            var familia = await context.Familias.Include(x=>x.Productos).FirstOrDefaultAsync(x=>x.Id==id);
            if (familia == null)
            {
                return NotFound();
            }
            context.RemoveRange(familia.Productos);
            context.Remove(familia); //OJO, borra en cascada si la BBDD esta configurada así.

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("logico/{id:int}")]
        public async Task<ActionResult> DeleteFamiliasLogico(int id)
        {

            var familia = await context.Familias.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (familia == null)
            {
                return NotFound();
            }
            familia.Eliminado = true;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("restaurar/{id:int}")]
        public async Task<ActionResult> RestaurarLogico(int id)
        {

            var familia = await context.Familias.AsTracking().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id); //Ponemos el ignorequeryFilters para que no tenga en cuenta el filtro puesto en familiaconfig para que solo cargue los no eliminados.
            if (familia == null)
            {
                return NotFound();
            }
            familia.Eliminado = false;

            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
