namespace AINouveau.Client.Models;

public class FilterData
{
    public bool Painting { get; set; }
    public bool DigitalArt { get; set; }
    public bool Drawing { get; set; }
    public bool Photography { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }

    // true if ALL type filters are false
    public bool NoTypeFilters => Painting == false && DigitalArt == false && Drawing == false && Photography == false;

    // true if ALL price filters are false
    public bool NoPriceFilters => MinPrice == null && MaxPrice == null;
}
