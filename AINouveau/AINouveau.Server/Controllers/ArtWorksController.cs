using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AINouveau.Shared;
using System.Text.Json;
using AINouveau.Server.Services;

namespace AINouveau.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtworksController : ControllerBase
{
    readonly IArtworkService artworkService;

    public ArtworksController(IArtworkService _artworkService)
    {
        artworkService = _artworkService;
    }

    // GET: api/artworks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Artwork>>> GetArtWork()
    {
        var artworks = await artworkService.GetAllArtwork();

        if (artworks == null)
        {
            return NotFound();
        }

        return artworks;
    }

    // GET: api/artworks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Artwork>> GetArtWork(int id)
    {
        var artwork = await artworkService.GetArtwork(id);

        if (artwork == null)
        {
            return NotFound();
        }

        return artwork;
    }

    // DELETE: api/artworks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtWork(int id)
    {
        var isDeleted = await artworkService.RemoveArtwork(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/artworks/5
    [HttpDelete]
    public async Task<IActionResult> DeleteAllArtWork()
    {
        var isDeleted = await artworkService.RemoveAllArtwork();

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}