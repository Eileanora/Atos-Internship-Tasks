using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<User>
{
    // public DataContext(DbContextOptions<DataContext> options)
    //     : base(options)
    // {
    // }
    private readonly IUserContext _userContext;

    public DataContext(DbContextOptions<DataContext> options, IUserContext userContext)
        : base(options)
    {
        _userContext = userContext;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        // SetGlobalQueryFilters(modelBuilder);
    }

    /*
     * This attempt failed to work because owner table has so many different scenarios as a user table
     * Which cannot be forced to adhere to a query filter for every scenario.
     * Especially UserManager queries which i don't have control over.
     */
    private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        var userId = _userContext.UserId.ToString();
        var isAdmin = _userContext.IsAdmin;
        
        modelBuilder.Entity<Pokemon>()
            .HasQueryFilter(p => isAdmin);
        
        modelBuilder.Entity<Owner>()
            .HasQueryFilter(o => isAdmin || o.UserId == userId);
    }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
