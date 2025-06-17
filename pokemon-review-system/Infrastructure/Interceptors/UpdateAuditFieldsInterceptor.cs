using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace   Infrastructure.Interceptors;
// TODO: Come back and make a specific setter for each entity type
public sealed class UpdateAuditFieldsInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
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
        
        var isLoggedIn = userContext.IsAuthenticated;
        Guid? currentUserId = isLoggedIn ? userContext.UserId : null;
        
        foreach (var entityEntry in entries)
        {
            SetAuditFields(entityEntry, currentUserId);
        }
        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    void SetAuditFields(EntityEntry<BaseEntity> entityEntry,Guid? currentUserId)
    {
        if (entityEntry.State == EntityState.Added)
        {
            entityEntry.Property(e => e.CreatedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
            if (currentUserId != Guid.Empty)
            {
                entityEntry.Property(e => e.CreatedBy).CurrentValue = currentUserId;
                entityEntry.Property(e => e.ModifiedBy).CurrentValue = currentUserId;
            }
        }
        if (entityEntry.State == EntityState.Modified)
        {
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.CreatedDate).IsModified = false;
            entityEntry.Property(e => e.CreatedBy).IsModified = false;
            entityEntry.Property(e => e.ModifiedBy).CurrentValue = currentUserId;
        }
    }
}
