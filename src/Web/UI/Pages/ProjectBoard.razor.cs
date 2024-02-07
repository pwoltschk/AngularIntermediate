using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using UI.Components.WorkItems;

namespace UI.Pages;

public partial class ProjectBoard
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    [Inject]
    public IWorkItemClient WorkItemClient { get; set; } = null!;
    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;
    [Inject]
    public IProjectClient ProjectsClient { get; set; } = null!;
    [Inject]
    public IAuthorizationService AuthorizationService { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    private List<ProjectDto> _projects = new();
    private List<UserDto> _users = new();
    private WorkItemDto _newWorkItem = new();
    private WorkItemDto _editWorkItem = new();
    private CreateWorkItemDialog _createWorkItemDialog = null!;
    private WorkItemDialog _editWorkItemDialog = null!;

    protected override async Task OnInitializedAsync()
    {
        var canLoadUser = await CheckUserPermissionsAsync();
        if (canLoadUser)
        {
            await LoadUsers();
        }
        await LoadProjects();
    }

    private async Task<bool> CheckUserPermissionsAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity?.IsAuthenticated == true)
        {
            var result = await AuthorizationService.AuthorizeAsync(user, "ReadUsers");
            return result.Succeeded;
        }
        return false;
    }

    private async Task LoadUsers()
    {
        _users = (await UsersClient.GetUsersAsync()).Users.ToList();
    }

    private async Task LoadProjects()
    {
        _projects = (await ProjectsClient.GetProjectsAsync()).Projects.ToList();
    }

    private string GetStageName(int stage) => stage switch
    {
        0 => "Planned",
        1 => "In Progress",
        2 => "Completed",
        _ => "Unknown"
    };

    public void ShowCreateWorkItemDialog()
    {
        _newWorkItem = new WorkItemDto { ProjectId = State.SelectedList!.Id };
        _createWorkItemDialog.Show();
    }

    public void ShowEditWorkItemDialog(WorkItemDto item)
    {
        _editWorkItem = item;
        _editWorkItemDialog.Show();
    }

    public async Task AddWorkItem(WorkItemDto workItem)
    {
        var itemId = await WorkItemClient.PostWorkItemAsync(new CreateWorkItemRequest
        {
            ProjectId = workItem.ProjectId,
            Title = workItem.Title
        });
        workItem.Id = itemId;
        workItem.ProjectId = State.SelectedList!.Id;
        State.SelectedList!.WorkItems.Add(workItem);
        StateHasChanged();
    }

    public async Task UpdateWorkItem(WorkItemDto workItem)
    {
        await SendUpdateWorkItemRequest(workItem);
        StateHasChanged();
    }

    private async Task SendUpdateWorkItemRequest(WorkItemDto workItem)
    {
        await WorkItemClient.PutWorkItemAsync(workItem.Id, new UpdateWorkItemRequest
        {
            Id = workItem.Id,
            ProjectId = workItem.ProjectId,
            Title = workItem.Title,
            Description = workItem.Description ?? string.Empty,
            Iteration = workItem.Iteration ?? string.Empty,
            AssignedTo = workItem.AssignedTo ?? string.Empty,
            Priority = workItem.Priority,
            StartDate = workItem.StartDate,
            Stage = workItem.Stage,
        });
    }

    public async Task DeleteWorkItem(int id)
    {
        await WorkItemClient.DeleteWorkItemAsync(id);
        var workItem = State.SelectedList!.WorkItems.First(w => w.Id == id);
        State.SelectedList!.WorkItems.Remove(workItem);
        StateHasChanged();
    }

    private async Task UpdateWorkItemStage(WorkItemDto item, int stage)
    {
        item.Stage = stage;
        await SendUpdateWorkItemRequest(item);
        StateHasChanged();
    }
}