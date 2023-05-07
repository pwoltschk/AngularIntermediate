using Microsoft.AspNetCore.Components;

namespace UI.Pages
{
    public partial class RoleManager
    {
        [Inject]
        public IRolesClient RolesClient { get; set; } = null!;

        public RolesViewModel? Model { get; set; }

        private string newRoleName = string.Empty;

        bool showEditDialog = false;
        protected override async Task OnInitializedAsync()
        {
            await LoadRoles();
        }

        private async Task LoadRoles()
        {
            Model = await RolesClient.GetRolesAsync();
        }

        void CloseEditDialog()
        {
            showEditDialog = false;
        }

        void OpenEditDialog(RoleDto role)
        {
            showEditDialog = true;
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

        private async Task DeleteRole(RoleDto role)
        {
            await RolesClient.DeleteRoleAsync(role.Id);

            await LoadRoles();
        }
    }
}
