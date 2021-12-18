using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Project> Projects { get; }

        DbSet<WorkItem> WorkItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
