using AINouveau.Shared;

namespace AINouveau.Server.Services;

public interface IArtworkService
{
    Task<List<Artwork>> GetAllArtwork();
    Task<Artwork?> GetArtwork(int id);
    Task<bool> RemoveArtwork(int id);
    Task<bool> RemoveAllArtwork();
}