using Domain.Entities;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
internal class ProjectRepository : IRepository<Project>
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Project>().FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Project>().Include(p => p.WorkItems).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(Project entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<Project>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Project entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<Project>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Project entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<Project>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
