using Application.Common.Exceptions;
using Application.Common.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Identity;

public class IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    : IIdentityService
{
    public async Task DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await userManager.DeleteAsync(user);
        }
    }

    public async Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.ToListAsync(cancellationToken);
        var roleList = new List<Role>();

        foreach (var role in roles)
        {
            var claims = await roleManager.GetClaimsAsync(role);
            var permissions = claims
                .Where(c => c.Type == nameof(Permission))
                .Select(c => c.Value)
                .ToList();

            if (role.Name != null)
            {
                roleList.Add(new Role(role.Id, role.Name, permissions));
            }
        }

        return roleList;
    }

    public async Task<IList<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var identityUsers = await userManager.Users.OrderBy(u => u.UserName).ToListAsync(cancellationToken);

        var users = new List<User>();
        foreach (var identityUser in identityUsers)
        {
            var user = await GetUserAsync(identityUser.Id);
            users.Add(user);
        }

        return users;
    }

    public async Task<User> GetUserAsync(string id)
    {
        var identityUser = await userManager.FindByIdAsync(id);

        if (identityUser is not { UserName: not null, Email: not null })
        {
            throw new NotFoundException(nameof(User), id);
        }

        var user = new User(identityUser.Id, identityUser.UserName, new Email(identityUser.Email));
        var roles = await userManager.GetRolesAsync(identityUser);

        foreach (var role in roles)
        {
            var identityRole = await roleManager.FindByNameAsync(role);


            if (identityRole?.Name == null)
            {
                continue;
            }

            var claims = await roleManager.GetClaimsAsync(identityRole);
            var permissions = claims
                .Where(c => c.Type == nameof(Permission))
                .Select(c => c.Value)
                .ToList();


            user.Roles.Add(new Role(identityRole.Id, identityRole.Name, permissions));

        }

        return user;
    }

    public async Task CreateRoleAsync(Role role)
    {
        await roleManager.CreateAsync(new IdentityRole { Name = role.Name });
    }

    public async Task UpdateRoleAsync(Role role)
    {
        var currentRole = await roleManager.FindByIdAsync(role.Id) ?? throw new NotFoundException(nameof(Role), role.Id);

        foreach (var claim in await roleManager.GetClaimsAsync(currentRole))
        {
            await roleManager.RemoveClaimAsync(currentRole, claim);
        }

        foreach (var permission in role.Permissions)
        {
            await roleManager.AddClaimAsync(currentRole, new Claim(nameof(Permission), permission));
        }

        currentRole.Name = role.Name;

        await roleManager.UpdateAsync(currentRole);
    }

    public async Task DeleteRoleAsync(string id)
    {
        var role = await roleManager.FindByIdAsync(id) ?? throw new NotFoundException(nameof(Role), id);
        await roleManager.DeleteAsync(role);
    }

    public async Task UpdateUserAsync(User user)
    {
        var currentUser = await userManager.FindByIdAsync(user.Id) ?? throw new NotFoundException(nameof(User), user.Name);

        currentUser.UserName = user.Name;
        currentUser.Email = user.Email.Value;

        await userManager.RemoveFromRolesAsync(currentUser, await userManager.GetRolesAsync(currentUser));

        foreach (var role in user.Roles)
        {
            await userManager.AddToRoleAsync(currentUser, role.Name);
        }

        await userManager.UpdateAsync(currentUser);
    }
}
