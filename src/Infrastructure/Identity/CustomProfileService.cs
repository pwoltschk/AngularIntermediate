using Application.Common.Exceptions;
using Domain.Entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Identity;

public class CustomProfileService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);

        if (user is not { Id: not null, UserName: not null })
        {
            throw new NotFoundException(nameof(User), string.Empty);
        }

        var claims = new List<Claim> { new("sub", user.Id), new("name", user.UserName) };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        context.IsActive = user != null;
    }
}