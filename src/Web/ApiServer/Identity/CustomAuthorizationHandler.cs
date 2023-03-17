using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiServer.Identity
{

    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            var permissionClaim = context.User.FindFirst(c => c.Type == nameof(Permission));

            if (permissionClaim == null)
            {
                return Task.CompletedTask;
            }

            var userPermission = permissionClaim.Value;

            if (userPermission == requirement.Permission)
            {
                context.Succeed(requirement);   
            }

            return Task.CompletedTask;
        }
    }

}
