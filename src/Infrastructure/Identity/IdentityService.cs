using Application.Common.Exceptions;
using Application.Common.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.DeleteAsync(user);
    }

    public async Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        var roleList = new List<Role>();

        foreach (var role in roles)
        {
            var claims = await _roleManager.GetClaimsAsync(role);
            var permissions = claims
                .Where(c => c.Type == nameof(Permission))
                .Select(c => c.Value)
                .ToList();

            roleList.Add(new Role(role.Id, role.Name, permissions));
        }

        return roleList;
    }

    public async Task<IList<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var identityUsers = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync(cancellationToken);

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
        var identityUser = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException(nameof(User), id);
        var user = new User(identityUser.Id, identityUser.UserName, new Email(identityUser.Email));
        var roles = await _userManager.GetRolesAsync(identityUser);

        foreach (var role in roles)
        {
            var identityRole = await _roleManager.FindByNameAsync(role);
            var claims = await _roleManager.GetClaimsAsync(identityRole);
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
        await _roleManager.CreateAsync(new IdentityRole { Name = role.Name });
    }

    public async Task UpdateRoleAsync(Role role)
    {
        var currentRole = await _roleManager.FindByIdAsync(role.Id) ?? throw new NotFoundException(nameof(Role), role.Id);

        foreach (var claim in await _roleManager.GetClaimsAsync(currentRole))
        {
            await _roleManager.RemoveClaimAsync(currentRole, claim);
        }

        foreach (var permission in role.Permissions)
        {
            await _roleManager.AddClaimAsync(currentRole, new Claim(nameof(Permission), permission));
        }

        currentRole.Name = role.Name;

        await _roleManager.UpdateAsync(currentRole);
    }

    public async Task DeleteRoleAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? throw new NotFoundException(nameof(Role), id);
        await _roleManager.DeleteAsync(role);
    }

    public async Task UpdateUserAsync(User user)
    {
        var currentUser = await _userManager.FindByIdAsync(user.Id) ?? throw new NotFoundException(nameof(User), user.Name);

        currentUser.UserName = user.Name;
        currentUser.Email = user.Email.Value;

        await _userManager.RemoveFromRolesAsync(currentUser, await _userManager.GetRolesAsync(currentUser));

        foreach (var role in user.Roles)
        {
            await _userManager.AddToRoleAsync(currentUser, role.Name);
        }

        await _userManager.UpdateAsync(currentUser);
    }
}
