using Microsoft.AspNetCore.Authorization;

namespace UI.Identity;

public class CustomAuthorizationRequirement : IAuthorizationRequirement
{
    public CustomAuthorizationRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}

