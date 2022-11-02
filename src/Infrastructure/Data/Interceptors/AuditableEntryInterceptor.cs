using Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Application.Common.Services;

namespace Infrastructure.Data.Interceptors
{
    public class AuditableEntryInterceptor : SaveChangesInterceptor
    {
        private readonly IUserInfo _userInfo;

        public AuditableEntryInterceptor(IUserInfo userInfo)
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
            var addedOrUpdatedEntries = context.ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

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
}
