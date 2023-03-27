using Microsoft.AspNetCore.Mvc;
using AINouveau.Shared;
using AINouveau.Server.Services;
using AINouveau.Shared.Models;

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
    public async Task<ActionResult<IEnumerable<Artwork>>> GetAllArtWork()
    {
        var artworks = await artworkService.GetAllArtwork();

        if (artworks == null)
        {
            return NotFound();
        }

        return artworks;
    }

    // GET: api/artworks/page
    [HttpGet("page")]
    public async Task<ActionResult<ArtworkResult>> GetArtworkForPage(bool painting, bool digitalArt,
        bool drawing, bool photograph, int? minPrice, int? maxPrice, int pageNumber)
    {
        var artworkResult = await artworkService.GetArtworkForPage(painting, digitalArt, drawing, photograph, minPrice, maxPrice, pageNumber);

        if (artworkResult == null)
        {
            return NotFound();
        }

        return artworkResult;
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