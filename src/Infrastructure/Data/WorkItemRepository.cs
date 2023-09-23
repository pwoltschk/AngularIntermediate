using Domain.Entities;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
internal class WorkItemRepository : IRepository<WorkItem>
{
    private readonly ApplicationDbContext _dbContext;

    public WorkItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<WorkItem>().FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<WorkItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<WorkItem>().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(WorkItem entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<WorkItem>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(WorkItem entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<WorkItem>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkItem entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<WorkItem>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
