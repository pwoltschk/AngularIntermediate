using Microsoft.AspNetCore.Authorization;

namespace Shared.Identity;

public class CustomAuthorizationRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}