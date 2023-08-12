using Microsoft.AspNetCore.Components;
namespace UI.Identity;

public class AuthorizeWrapper : Microsoft.AspNetCore.Components.Authorization.AuthorizeView
{
    private IEnumerable<string>? _permissions;

    [Parameter]
    public IEnumerable<string> Permissions
    {
        get => _permissions;
        set
        {
            _permissions = value;
            Policy = _permissions != null && _permissions.Any()
                ? string.Join("|", _permissions.Select(p => p.ToString()))
                : string.Empty;
        }
    }
}

