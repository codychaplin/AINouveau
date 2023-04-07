using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using AINouveau.Shared;
using AINouveau.Server.Data;
using AINouveau.Shared.Models;

namespace AINouveau.Server.Services;

public class ArtworkService : IArtworkService
{
    readonly AINouveauDbContext dbContext;
    const int PAGE_SIZE = 12;

    public ArtworkService(AINouveauDbContext context)
    {
        dbContext = context;
    }

    async Task Init()
    {
        // if dbset is empty, get Artwork list from json file and add to db
        if (!dbContext.Artwork.Any())
        {
            string jsonText = await File.ReadAllTextAsync("Data/Artworks.json");
            var artworks = JsonSerializer.Deserialize<List<Artwork>>(jsonText)!;
            await dbContext.Artwork.AddRangeAsync(artworks);
            await dbContext.SaveChangesAsync();
        }
    }
    
    public async Task<List<Artwork>> GetAllArtwork()
    {
        await Init();
        return await dbContext.Artwork.ToListAsync();
    }

    public async Task<ArtworkResult> GetArtworkForPage(bool painting, bool digitalArt, bool drawing,
        bool photograph, int? minPrice, int? maxPrice, int pageNumber, SortOptions option)
    {
        await Init();
        var query = dbContext.Artwork.AsQueryable();

        // filter by type
        if (!(painting && digitalArt && drawing && photograph))
        {
            if (painting || digitalArt || drawing || photograph)
            {
                query = query.Where(a => (painting && a.Type == "Painting")
                                      || (digitalArt && a.Type == "DigitalArt")
                                      || (drawing && a.Type == "Drawing")
                                      || (photograph && a.Type == "Photograph"));
            }
        }

        // filter by price
        if (minPrice.HasValue)
            query = query.Where(a => a.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(a => a.Price <= maxPrice.Value);

        // sort by SortOption
        List<Artwork> artworks = query.ToList();
        switch (option)
        {
            case SortOptions.Popular:
                break;
            case SortOptions.PriceLowToHigh:
                artworks = artworks.OrderBy(a => a.Price).ToList();
                break;
            case SortOptions.PriceHighToLow:
                artworks = artworks.OrderByDescending(a => a.Price).ToList();
                break;
            case SortOptions.NameAToZ:
                artworks = artworks.OrderBy(a => a.Name).ToList();
                break;
            case SortOptions.NameZToA:
                artworks = artworks.OrderByDescending(a => a.Name).ToList();
                break;
            default:
                break;
        }

        // get count and pagination
        var count = query.Count();
        var queryForPage = artworks.Skip((pageNumber - 1) * PAGE_SIZE)
                                   .Take(PAGE_SIZE)
                                   .ToList();

        return new ArtworkResult(queryForPage, count);
    }

    public async Task<List<Artwork>> GetSimilarArtwork(Artwork artwork)
    {
        // gets 4 random artworks of same type
        return await dbContext.Artwork.Where(a => a.Id != artwork.Id && a.Type == artwork.Type)
                                      .OrderBy(a => EF.Functions.Random())
                                      .Take(4)
                                      .ToListAsync();
    }

    public async Task<Artwork?> GetArtwork(int id)
    {
        return await dbContext.Artwork.FindAsync(id);
    }

    public async Task<bool> RemoveArtwork(int id)
    {
        var artWork = await dbContext.Artwork.FindAsync(id);
        if (artWork == null)
        {
            return false;
        }

        dbContext.Artwork.Remove(artWork);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveAllArtwork()
    {
        var artworks = await dbContext.Artwork.ToListAsync();
        if (artworks == null || !artworks.Any())
        {
            return false;
        }

        dbContext.Artwork.RemoveRange(artworks);
        await dbContext.SaveChangesAsync();
        return true;
    }
}