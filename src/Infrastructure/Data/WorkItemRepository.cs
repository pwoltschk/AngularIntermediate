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

    public async Task<WorkItem?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<WorkItem>().FindAsync(id);
    }

    public async Task<IEnumerable<WorkItem>> GetAllAsync()
    {
        return await _dbContext.Set<WorkItem>().ToListAsync();
    }

    public async Task AddAsync(WorkItem entity)
    {
        await _dbContext.Set<WorkItem>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(WorkItem entity)
    {
        _dbContext.Set<WorkItem>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkItem entity)
    {
        _dbContext.Set<WorkItem>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
