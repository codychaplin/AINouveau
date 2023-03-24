using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AINouveau.Server.Data;
using AINouveau.Shared;
using System.Text.Json;

namespace AINouveau.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtworksController : ControllerBase
{
    private readonly AINouveauDbContext dbContext;

    public ArtworksController(AINouveauDbContext context)
    {
        dbContext = context;
    }

    // GET: api/artworks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Artwork>>> GetArtWork()
    {
        try
        {
            await Task.Delay(500);
            string jsonText = System.IO.File.ReadAllText("Data/Artworks.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var artworks = JsonSerializer.Deserialize<List<Artwork>>(jsonText, options);
            return artworks;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        /*if (dbContext.Artwork == null)
        {
            return NotFound();
        }
        return await dbContext.Artwork.ToListAsync();*/
    }


    // GET: api/artworks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Artwork>> GetArtWork(int id)
    {
        if (dbContext.Artwork == null)
        {
            return NotFound();
        }
        var Artwork = await dbContext.Artwork.FindAsync(id);

        if (Artwork == null)
        {
            return NotFound();
        }

        return Artwork;
    }

    private bool ArtWorkExists(int id)
    {
        return (dbContext.Artwork?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
