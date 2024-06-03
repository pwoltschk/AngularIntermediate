using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Shared.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace UI.Identity;

public class PermissionAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
    : AccountClaimsPrincipalFactory<RemoteUserAccount>(accessor)
{
    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        if (account is null)
            return new ClaimsPrincipal();

        var user = await base.CreateUserAsync(account, options);
        var identity = user.Identity as ClaimsIdentity;

        if (!account.AdditionalProperties.TryGetValue(nameof(Permission), out object? permissions))
        {
            return user;
        }

        var permissionsJson = permissions as JsonElement?;

        permissionsJson?
            .EnumerateArray()
            .ToList()
            .ForEach(permission => identity?
                .AddClaim(new Claim(nameof(Permission), permission.GetString() ?? string.Empty)));

        return user;
    }

}