using Microsoft.AspNetCore.Components;
namespace UI.Identity;

public class AuthorizeWrapper : Microsoft.AspNetCore.Components.Authorization.AuthorizeView
{
    private IEnumerable<string>? _permissions;

    [Parameter]
    public IEnumerable<string> Permissions
    {
        get => _permissions ?? Array.Empty<string>();
        set
        {
            _permissions = value;
            IEnumerable<string> permissions = _permissions.ToList();
            Policy = _permissions != null && permissions.Any()
                ? string.Join("|", permissions.Select(p => p.ToString()))
                : string.Empty;
        }
    }
}

