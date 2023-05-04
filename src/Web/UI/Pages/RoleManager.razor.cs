using Microsoft.AspNetCore.Components;

namespace UI.Pages
{
    public partial class RoleManager
    {
        [Inject]
        public IRolesClient RolesClient { get; set; } = null!;

        public RolesViewModel? Model { get; set; }

        private string newRoleName = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadRoles();
        }

        private async Task LoadRoles()
        {
            Model = await RolesClient.GetRolesAsync();
        }

        private async Task AddRole()
        {
            if (!string.IsNullOrWhiteSpace(newRoleName))
            {
                var newRole = new RoleDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = newRoleName,
                    Permissions = new List<string>()
                };

                await RolesClient.PostRoleAsync(newRole);

                newRoleName = string.Empty;

                await LoadRoles();
            }
        }
    }
}
