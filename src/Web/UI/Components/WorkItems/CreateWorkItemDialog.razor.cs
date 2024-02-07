using Microsoft.AspNetCore.Components;

namespace UI.Components.WorkItems;

public partial class CreateWorkItemDialog
{
    [Parameter]
    public string Title { get; set; } = "Create Work Item";

    [Parameter]
    public WorkItemDto WorkItem { get; set; } = new();

    [Parameter]
    public List<ProjectDto> Projects { get; set; } = new();

    [Parameter]
    public EventCallback<WorkItemDto> OnSave { get; set; }

    private bool IsVisible { get; set; }

    public void Show()
    {
        IsVisible = true;
        StateHasChanged();
    }

    private void Close()
    {
        IsVisible = false;
    }

    private async Task SaveWorkItem()
    {
        await OnSave.InvokeAsync(WorkItem);
        Close();
    }
}