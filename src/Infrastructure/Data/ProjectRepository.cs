using Domain.Entities;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
internal class ProjectRepository(ApplicationDbContext dbContext) : IRepository<Project>
{
    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Project>().FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<Project>().Include(p => p.WorkItems).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(Project entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<Project>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Project entity, CancellationToken cancellationToken)
    {
        dbContext.Set<Project>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Project entity, CancellationToken cancellationToken)
    {
        dbContext.Set<Project>().Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
