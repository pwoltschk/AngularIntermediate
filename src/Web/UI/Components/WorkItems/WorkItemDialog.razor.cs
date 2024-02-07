using Microsoft.AspNetCore.Components;

namespace UI.Components.WorkItems;

public class WorkItemDialogBase : ComponentBase
{
    [Parameter]
    public List<ProjectDto> Projects { get; set; } = new();

    [Parameter]
    public List<UserDto> Users { get; set; } = new();

    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public WorkItemDto WorkItem { get; set; } = null!;

    [Parameter]
    public EventCallback<WorkItemDto> OnSave { get; set; }

    protected bool IsVisible { get; private set; }

    protected string? SelectedUserId { get; set; }

    protected override void OnParametersSet()
    {
        if (Users is not { Count: > 0 })
        {
            return;
        }
        var selectedUser = Users.FirstOrDefault(u => u.Name == WorkItem.AssignedTo);
        SelectedUserId = selectedUser?.Id;
    }

    public void Show()
    {
        IsVisible = true;
        StateHasChanged();
    }

    protected void Close()
    {
        IsVisible = false;
        StateHasChanged();
    }

    private void UpdateAssignedTo()
    {
        var selectedUser = Users.FirstOrDefault(u => u.Id == SelectedUserId);
        WorkItem.AssignedTo = selectedUser?.Name;
    }

    protected async Task SaveWorkItem()
    {
        UpdateAssignedTo();
        await OnSave.InvokeAsync(WorkItem);
        Close();
    }
}