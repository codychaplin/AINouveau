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
        if (dbContext.Artwork == null)
        {
            return NotFound();
        }

        if (!dbContext.Artwork.Any())
        {
            // gets Artwork list from json file and adds list to db
            string jsonText = await System.IO.File.ReadAllTextAsync("Data/Artworks.json");
            var artworks = JsonSerializer.Deserialize<List<Artwork>>(jsonText)!;
            await dbContext.Artwork.AddRangeAsync(artworks);
            await dbContext.SaveChangesAsync();
        }

        return await dbContext.Artwork.ToListAsync();
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

    private bool ArtworkExists(int id)
    {
        return (dbContext.Artwork?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
