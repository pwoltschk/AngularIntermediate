
using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class RoleManager
{
    [Inject]
    public IRolesClient RolesClient { get; set; } = null!;

    public RolesViewModel? Model { get; set; }

    private string _newRoleName = string.Empty;

    private RoleDto? _selectedRole;
    private bool _showEditDialog;
    protected override async Task OnInitializedAsync()
    {
        await LoadRoles();
    }

    private async Task LoadRoles()
    {
        Model = await RolesClient.GetRolesAsync();
    }

    private void CloseEditDialog()
    {
        _showEditDialog = false;
    }

    private void OpenEditDialog(RoleDto role)
    {
        _selectedRole = new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = new List<string>(role.Permissions)
        };
        _showEditDialog = true;
    }

    private void TogglePermission(string permission, bool isChecked)
    {
        if (isChecked)
        {
            _selectedRole?.Permissions.Add(permission);
        }
        else
        {
            _selectedRole?.Permissions.Remove(permission);
        }
    }

    private async Task SaveRole()
    {
        if (_selectedRole != null)
        {
            await RolesClient.PutRoleAsync(_selectedRole.Id, _selectedRole);
            await LoadRoles();
        }
        _showEditDialog = false;
    }

    private async Task AddRole()
    {
        if (!string.IsNullOrWhiteSpace(_newRoleName))
        {
            var newRole = new RoleDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = _newRoleName,
                Permissions = new List<string>()
            };

            await RolesClient.PostRoleAsync(newRole);

            _newRoleName = string.Empty;

            await LoadRoles();
        }
    }

    private async Task DeleteRole(RoleDto role)
    {
        await RolesClient.DeleteRoleAsync(role.Id);

        await LoadRoles();
    }
}