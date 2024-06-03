using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextInitialiser(
    ApplicationDbContext context,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    private const string Administrator = "Administrator";
    private const string Manager = "Manager";
    private const string AdminUser = "ADMIN@login.com";

    public async Task InitialiseAsync()
    {
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync();
        }
        else
        {
            await context.Database.EnsureCreatedAsync();
        }
    }

    public async Task SeedAsync()
    {
        if (await context.Projects.AnyAsync())
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
        await context.Projects.AddRangeAsync(projects);
        await context.WorkItems.AddAsync(unassignedItem);

        await CreateRole(Administrator, [.. Permission.AllPermissions]);
        await CreateRole(Manager, Permission.WriteProjects, Permission.ReadProjects);

        var adminUser = new IdentityUser { UserName = AdminUser, Email = AdminUser };
        var password = "AdminUser777!";

        await userManager.CreateAsync(adminUser, password);

        await userManager.UpdateAsync(adminUser);

        await userManager.AddToRoleAsync(adminUser, Administrator);
        await context.SaveChangesAsync();
    }

    private async Task CreateRole(string roleName, params string[] permissions)
    {
        var role = new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() };
        await roleManager.CreateAsync(role);

        foreach (var permission in permissions)
        {
            await roleManager.AddClaimAsync(role, Permission.ToClaim(permission));
        }
    }
}