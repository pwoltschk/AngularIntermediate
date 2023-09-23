using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class UserEditor
{
    [Parameter]
    public string UserId { get; set; } = null!;

    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    public IRolesClient RolesClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public UserDetailsViewModel? Model { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Model = await UsersClient.GetUserAsync(UserId);
    }

    public void ToggleSelectedRole(string roleName)
    {
        if (Model!.Roles.Any(role => role.Name == roleName))
        {
            Model!.User.Roles.Remove(roleName);
        }
        else
        {
            Model.User.Roles.Add(roleName);
        }

        StateHasChanged();
    }

    public async Task UpdateUser()
    {
        await UsersClient.PutUserAsync(Model!.User.Id, Model.User);

        Navigation.NavigateTo("/users");
    }

    public void CancelEdit()
    {
        Navigation.NavigateTo("/users");
    }
}