using Domain.Entities;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
internal class WorkItemRepository(ApplicationDbContext dbContext) : IRepository<WorkItem>
{
    public async Task<WorkItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<WorkItem>().FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<WorkItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<WorkItem>().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(WorkItem entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<WorkItem>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(WorkItem entity, CancellationToken cancellationToken)
    {
        dbContext.Set<WorkItem>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkItem entity, CancellationToken cancellationToken)
    {
        dbContext.Set<WorkItem>().Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
