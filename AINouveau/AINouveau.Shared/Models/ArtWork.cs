using System.ComponentModel.DataAnnotations;

namespace AINouveau.Shared;

public class Artwork
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [Required]
    public decimal Price { get; set; }

    [Required, MaxLength (255)]
    public string Prompt { get; set; } = "";

    [Required, MaxLength(50)]
    public string ImageUrl { get; set; } = "";

    [Required, MaxLength(50)]
    public string Type { get; set; } = "";
}