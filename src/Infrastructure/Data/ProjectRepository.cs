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

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<Project>().FindAsync(id);
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbContext.Set<Project>().Include(p => p.WorkItems).ToListAsync();
    }

    public async Task AddAsync(Project entity)
    {
        await _dbContext.Set<Project>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Project entity)
    {
        _dbContext.Set<Project>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Project entity)
    {
        _dbContext.Set<Project>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
