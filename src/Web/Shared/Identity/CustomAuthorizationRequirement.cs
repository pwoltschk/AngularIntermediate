using Microsoft.AspNetCore.Authorization;

namespace Shared.Identity;

public class CustomAuthorizationRequirement : IAuthorizationRequirement
{
    public CustomAuthorizationRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}