using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class UserManager
{
    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public UsersViewModel Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadUsers();
    }

    public void EditUser(string id) => NavigationManager.NavigateTo("/users/" + id);

    private async Task LoadUsers()
    {
        Model = await UsersClient.GetUsersAsync();
    }

    private async Task DeleteUser(UserDto user)
    {
        await UsersClient.DeleteUserAsync(user.Id);
        await LoadUsers();
    }
}