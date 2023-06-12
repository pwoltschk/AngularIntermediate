namespace Shared.Identity;

public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
{
    public AuthorizeAttribute(params string[] permissions)
    {
        Policy = permissions?.Any() == true ? string.Join("|", permissions) : string.Empty;
    }
}
