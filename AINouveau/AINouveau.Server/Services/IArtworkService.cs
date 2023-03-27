using AINouveau.Shared;

namespace AINouveau.Server.Services;

public interface IArtworkService
{
    Task<List<Artwork>> GetAllArtwork();
    Task<List<Artwork>> GetArtworkForPage(bool painting, bool digitalArt, bool drawing,
        bool photograph, int? minPrice, int? maxPrice, int pageNumber);
    Task<Artwork?> GetArtwork(int id);
    Task<bool> RemoveArtwork(int id);
    Task<bool> RemoveAllArtwork();
}