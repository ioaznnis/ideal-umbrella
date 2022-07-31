// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

Console.WriteLine("Hello, World!");

public class Entity
{
    public Guid Id { get; set; }
    
    public NpgsqlRange<decimal> Range { get; set; }
}

public class ExampleDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("connectionString");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>().HasIndex(entity => entity.Range).IsUnique();
    }
}