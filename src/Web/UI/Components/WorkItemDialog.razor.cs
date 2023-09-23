using Microsoft.AspNetCore.Components;

namespace UI.Components;

public partial class WorkItemDialog
{
    [Parameter]
    public List<ProjectDto> Projects { get; set; } = new();

    [Parameter]
    public List<UserDto> Users { get; set; } = new();

    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public WorkItemDto WorkItem { get; set; } = new();

    [Parameter]
    public EventCallback<WorkItemDto> OnSave { get; set; }

    public bool IsVisible { get; private set; }
    private string? SelectedUserId { get; set; }

    public void Show()
    {
        IsVisible = true;
        SelectedUserId = Users.FirstOrDefault(u => u.Name == WorkItem.AssignedTo)?.Id;
    }

    public void Close() => IsVisible = false;

    private void UpdateAssignedTo()
    {
        var selectedUser = Users.FirstOrDefault(u => u.Id == SelectedUserId);
        WorkItem.AssignedTo = selectedUser?.Name;
    }

    private async Task SaveWorkItem()
    {
        UpdateAssignedTo();
        await OnSave.InvokeAsync(WorkItem);
        Close();
    }
}