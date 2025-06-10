using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<Country> Countries { get; set; }
}
