using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.Entidades;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/familiasfactory")]
    public class FamiliasFactoryController : ControllerBase
    {
        private readonly IDbContextFactory<ApplicationDBContext> dbContextFactory;

        public FamiliasFactoryController(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<Familia>> GetFamilias()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return await context.Familias.ToListAsync();
            }

        }

    }
}
