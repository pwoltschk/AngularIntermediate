using Microsoft.AspNetCore.Components;
using UI.Components.WorkItems;

namespace UI.Pages;

public partial class WorkItems
{
    [Inject]
    public IWorkItemClient WorkItemClient { get; set; } = null!;

    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    public IProjectClient ProjectsClient { get; set; } = null!;

    private WorkItemsViewModel? Model { get; set; }

    private List<UserDto> _users = [];
    private List<ProjectDto> _projects = [];

    private WorkItemDto _newWorkItem = new();
    private WorkItemDto _editWorkItem = new();

    private CreateWorkItemDialog _createWorkItemDialog = null!;
    private WorkItemDialog _editWorkItemDialog = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadWorkItems();
        await LoadUsersAndProjects();
    }

    private async Task LoadWorkItems()
    {
        Model = await WorkItemClient.GetWorkItemsAsync();
    }

    private async Task LoadUsersAndProjects()
    {
        _users = [.. (await UsersClient.GetUsersAsync()).Users];
        _projects = [.. (await ProjectsClient.GetProjectsAsync()).Projects];
    }

    private static string GetStageName(int stage) => stage switch
    {
        0 => "Planned",
        1 => "In Progress",
        2 => "Completed",
        _ => "Unknown"
    };

    private void ShowCreateWorkItemDialog()
    {
        _newWorkItem = new WorkItemDto();
        _createWorkItemDialog.Show();
    }

    private void ShowEditWorkItemDialog(WorkItemDto workItem)
    {
        _editWorkItem = workItem;
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
        Model!.WorkItems.Add(workItem);
        StateHasChanged();
    }

    public async Task UpdateWorkItem(WorkItemDto workItem)
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
            Stage = workItem.Stage
        });
        await LoadWorkItems();
        StateHasChanged();
    }

    private async Task DeleteWorkItem(WorkItemDto workItem)
    {
        await WorkItemClient.DeleteWorkItemAsync(workItem.Id);
        Model!.WorkItems.Remove(workItem);
        StateHasChanged();
    }

    private string GetProjectName(int? projectId)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        return project != null ? project.Title : "UNTRACKED";
    }

    private static string Truncate(string value, int maxChars)
    {
        return string.IsNullOrEmpty(value) ? string.Empty : value.Length > maxChars ? value[..maxChars] + "..." : value;
    }
}