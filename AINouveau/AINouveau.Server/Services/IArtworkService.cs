using AINouveau.Shared;

namespace AINouveau.Server.Services;

public interface IArtworkService
{
    Task<List<Artwork>> GetAllArtwork();
    Task<Artwork?> GetArtwork(int id);
}