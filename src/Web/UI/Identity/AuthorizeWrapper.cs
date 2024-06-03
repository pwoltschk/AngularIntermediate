using Microsoft.AspNetCore.Components;

namespace UI.Identity
{
    public class AuthorizeWrapper : Microsoft.AspNetCore.Components.Authorization.AuthorizeView
    {
        [Parameter]
        public IEnumerable<string> Permissions { get; set; } = Array.Empty<string>();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Policy = Permissions.Any() ? string.Join("|", Permissions.Select(p => p.ToString())) : string.Empty;
        }
    }
}