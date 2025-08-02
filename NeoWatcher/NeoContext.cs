using Microsoft.EntityFrameworkCore;
using Neo_Watcher;

public class NeoContext : DbContext
{
    public NeoContext(DbContextOptions<NeoContext> options) : base(options) { }

    public DbSet<NearEarthObject> NearEarthObjects { get; set; }
}
