using Microsoft.AspNetCore.Components;

namespace UI.Pages
{
    public partial class RoleManager
    {
        [Inject]
        public IRolesClient RolesClient { get; set; } = null!;

        public RolesViewModel? Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadRoles();
        }

        private async Task LoadRoles()
        {
            Model = await RolesClient.GetRolesAsync();
        }
    }
}
