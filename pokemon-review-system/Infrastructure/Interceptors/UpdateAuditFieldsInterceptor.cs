using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace   Infrastructure.Interceptors;
// TODO: Come back and make a specific setter for each entity type
public sealed class UpdateAuditFieldsInterceptor : SaveChangesInterceptor
{
    // Attempt to use a dictionary to map entity types to their specific audit field setters (FAILED)
    // private readonly Dictionary<Type, Action<EntityEntry<BaseEntity>>> _entityAuditSetters = new()
    // {
    //     { typeof(Pokemon), entry => SetPokemonUnmodifiedPropertiesStatic((Pokemon)entry) },
    //     // Add more mappings here for other entity types as needed
    // };

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
        
        IEnumerable<EntityEntry<BaseEntity>> entries = dbContext
            .ChangeTracker
            .Entries<BaseEntity>();
        
        
        foreach (var entityEntry in entries)
        {
            SetEntitiesAuditFields(entityEntry);
        }
        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    void SetEntitiesAuditFields(EntityEntry<BaseEntity> entityEntry)
    {
        // if (_entityAuditSetters.TryGetValue(entityEntry.Entity.GetType(), out var setter))
        // {
        //     setter(entityEntry);
        // }

        if (entityEntry.State == EntityState.Added)
        {
            entityEntry.Property(e => e.CreatedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
        }
        if (entityEntry.State == EntityState.Modified)
        {
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.CreatedDate).IsModified = false;
        }
    }

    // Static version for dictionary delegate
    // private static void SetPokemonUnmodifiedPropertiesStatic(EntityEntry<Pokemon> entry)
    // {
    //     var pokemonEntry = entry.Cast<Pokemon>();
    //     pokemonEntry.Property(e => e.CreatedDate).IsModified = false;
    //     pokemonEntry.Property(e => e.Name).IsModified = false;
    // }
    // private static void SetPokemonUnmodifiedPropertiesStatic(EntityEntry<Pokemon> entityEntry)
    // {
    //     // Example: Prevent modification of CreatedDate for Pokemon
    //     entityEntry.Property(e => e.CreatedDate).IsModified = false;
    //     entityEntry.Property(e => e.Name).IsModified = false;
    // }
}
