using Application.Common.Services;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _userInfo;

    public AuditableEntityInterceptor(IUserContext userInfo)
    {
        _userInfo = userInfo;
    }

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

    public void AuditEntities(DbContext? context)
    {
        var addedOrUpdatedEntries = context.ChangeTracker.Entries<AuditableEntity>();


        foreach (var entry in addedOrUpdatedEntries)
        {
            var entity = entry.Entity as AuditableEntity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedBy = _userInfo.UserId;
                entity.CreatedOn = DateTime.UtcNow;
            }

            entity.UpdatedBy = _userInfo.UserId;
            entity.UpdatedOn = DateTime.UtcNow;
        }
    }
}