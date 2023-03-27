namespace AINouveau.Shared.Models;

public class ArtworkResult
{
    public List<Artwork> Artworks { get; set; }
    public int Count { get; set; }

    public ArtworkResult(List<Artwork> artworks, int count)
    {
        Artworks = artworks;
        Count = count;
    }
}
