using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.Entidades;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/familas")]
    public class FamiliasController :ControllerBase
    {
        private readonly ApplicationDBContext context;

        public FamiliasController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Familia>> Get()
        {
            return await context.Familias.ToListAsync();
        }
    }
}
