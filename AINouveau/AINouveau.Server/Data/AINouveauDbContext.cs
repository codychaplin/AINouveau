using Microsoft.EntityFrameworkCore;
using AINouveau.Shared;

namespace AINouveau.Server.Data;

public class AINouveauDbContext : DbContext
{
    public AINouveauDbContext(DbContextOptions<AINouveauDbContext> options) : base(options) { }

    public DbSet<Artwork> Artwork { get; set; } = default!;
}
