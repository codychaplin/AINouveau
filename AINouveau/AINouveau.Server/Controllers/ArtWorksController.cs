using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AINouveau.Server.Data;
using AINouveau.Shared;

namespace AINouveau.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorksController : ControllerBase
    {
        private readonly AINouveauDbContext dbContext;

        public ArtWorksController(AINouveauDbContext context)
        {
            dbContext = context;
        }

        // GET: api/ArtWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtWork>>> GetArtWork()
        {
          if (dbContext.ArtWork == null)
          {
              return NotFound();
          }
            return await dbContext.ArtWork.ToListAsync();
        }

        // GET: api/ArtWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWork>> GetArtWork(int id)
        {
          if (dbContext.ArtWork == null)
          {
              return NotFound();
          }
            var artWork = await dbContext.ArtWork.FindAsync(id);

            if (artWork == null)
            {
                return NotFound();
            }

            return artWork;
        }

        private bool ArtWorkExists(int id)
        {
            return (dbContext.ArtWork?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
