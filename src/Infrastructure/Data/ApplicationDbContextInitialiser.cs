using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Initialise()
    {
        if (_context.Database.IsSqlServer())
        {
            _context.Database.Migrate();
        }
        else
        {
            _context.Database.EnsureCreated();
        }
    }

    public void Seed()
    {
        if (_context.Projects.Any())
        {
            return;
        }

        var list = new Project
        {
            Title = "Develop Kanban board",
            WorkItems = new List<WorkItem>()
                {
                    new() { Title = "Implement Infrastructure Layer" },
                    new() { Title = "Implement Frontend" },
                }
        };

        _context.Projects.Add(list);
        _context.SaveChanges();
    }
}
