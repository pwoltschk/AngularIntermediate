using Application.Common.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private const string Administrator = "Administrator";
    private const string Manager = "Manager";
    private const string AdminUser = "ADMIN";

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

        await CreateRole(Administrator, Permission.AllPermissions.ToArray());
        await CreateRole(Manager, Permission.WriteProjects, Permission.ReadProjects);

        var adminUser = new IdentityUser { UserName = AdminUser, Email = AdminUser + "@login.com" };
        var password = AdminUser + "pw+4";
        await CreateUser(adminUser, password, Administrator);

        _context.SaveChanges();
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

    private async Task CreateUser(IdentityUser user, string password, string roleName)
    {
        await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, roleName);
    }
}
