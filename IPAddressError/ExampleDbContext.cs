using Microsoft.EntityFrameworkCore;

namespace IPAddressError;

public class ExampleDbContext : DbContext
{
    public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
    {
    }

    public DbSet<Entity> Entities { get; set; } = null!;
}