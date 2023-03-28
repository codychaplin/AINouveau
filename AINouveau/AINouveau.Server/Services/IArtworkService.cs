using AINouveau.Shared;
using AINouveau.Shared.Models;

namespace AINouveau.Server.Services;

public interface IArtworkService
{
    Task<List<Artwork>> GetAllArtwork();
    Task<ArtworkResult> GetArtworkForPage(bool painting, bool digitalArt, bool drawing,
        bool photograph, int? minPrice, int? maxPrice, int pageNumber, SortOptions option);
    Task<Artwork?> GetArtwork(int id);
    Task<bool> RemoveArtwork(int id);
    Task<bool> RemoveAllArtwork();
}