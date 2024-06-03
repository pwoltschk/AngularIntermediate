using Application.Common.Services;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(IUserContext userInfo) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AuditEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AuditEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        var addedOrUpdatedEntries = context.ChangeTracker.Entries<AuditableEntity>();


        foreach (var entry in addedOrUpdatedEntries)
        {
            var entity = entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedBy = userInfo.UserId;
                entity.CreatedOn = DateTime.UtcNow;
            }

            entity.UpdatedBy = userInfo.UserId;
            entity.UpdatedOn = DateTime.UtcNow;
        }
    }
}