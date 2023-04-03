using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace UI.Identity;

public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
    {
        var permissionClaims = context.User.FindAll(c => c.Type == nameof(Permission));

        foreach (var claim in permissionClaims)
        {
            if (claim.Value == requirement.Permission)
            {
                context.Succeed(requirement);
                break;
            }
        }

        return Task.CompletedTask;
    }
}
