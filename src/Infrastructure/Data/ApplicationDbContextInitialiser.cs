using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextInitialiser
{
    private const string Administrator = "Administrator";
    private const string Manager = "Manager";
    private const string AdminUser = "ADMIN@login.com";
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
        else
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

    public async Task SeedAsync()
    {
        if (await _context.Projects.AnyAsync())
        {
            return;
        }

        var unassignedItem = new WorkItem() { Title = "Analyse business workflow" };

        var workItems = new List<WorkItem>
        {
            new() { Title = "Implement Infrastructure Layer", Stage = Stage.InProgress, Description = "[] Add DbContext, [] Add Migrations", StartDate = DateTime.Today},
            new() { Title = "Implement Frontend", Description = "[] Add CSS, [] Implement State store", StartDate = DateTime.Today}
        };

        var projects = new List<Project>
        {
            new ()
            {
                Title = "Develop Kanban board",
                WorkItems = workItems
            },
            new ()
            {
                Title = "Empty Project",
            }
        };
        await _context.Projects.AddRangeAsync(projects);
        await _context.WorkItems.AddAsync(unassignedItem);

        await CreateRole(Administrator, [.. Permission.AllPermissions]);
        await CreateRole(Manager, Permission.WriteProjects, Permission.ReadProjects);

        var adminUser = new IdentityUser { UserName = AdminUser, Email = AdminUser };
        var password = "AdminUser777!";

        await _userManager.CreateAsync(adminUser, password);

        await _userManager.UpdateAsync(adminUser);

        await _userManager.AddToRoleAsync(adminUser, Administrator);
        await _context.SaveChangesAsync();
    }

    private async Task CreateRole(string roleName, params string[] permissions)
    {
        var role = new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() };
        await _roleManager.CreateAsync(role);

        foreach (var permission in permissions)
        {
            await _roleManager.AddClaimAsync(role, Permission.ToClaim(permission));
        }
    }
}