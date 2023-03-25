using Microsoft.EntityFrameworkCore;
using AINouveau.Shared;
using AINouveau.Server.Data;
using System.Text.Json;

namespace AINouveau.Server.Services;

public class ArtworkService : IArtworkService
{
    private readonly AINouveauDbContext dbContext;

    public ArtworkService(AINouveauDbContext context)
    {
        dbContext = context;
    }
    
    public async Task<List<Artwork>> GetAllArtwork()
    {
        if (!dbContext.Artwork.Any())
        {
            // gets Artwork list from json file and adds list to db
            string jsonText = await File.ReadAllTextAsync("Data/Artworks.json");
            var artworks = JsonSerializer.Deserialize<List<Artwork>>(jsonText)!;
            await dbContext.Artwork.AddRangeAsync(artworks);
            await dbContext.SaveChangesAsync();
        }

        return await dbContext.Artwork.ToListAsync();
    }

    public async Task<Artwork?> GetArtwork(int id)
    {
        return await dbContext.Artwork.FindAsync(id);
    }
}