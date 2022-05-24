using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //[HttpGet]
        //public async Task<IEnumerable<Familia>> GetFamiliasNoTracking()
        //{
        //    return await context.Familias.AsNoTracking().ToListAsync(); //Innecesario puesto que ya lo hemos puesto en Program.cs para el DBContext.
        //}

    }
}
