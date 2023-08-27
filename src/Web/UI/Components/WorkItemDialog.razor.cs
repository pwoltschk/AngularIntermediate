using Microsoft.AspNetCore.Components;

namespace UI.Components;
public partial class WorkItemDialog
{
    [Parameter]
    public List<UserDto> Users { get; set; } = new List<UserDto>(); 

    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public WorkItemDto WorkItem { get; set; } = new();

    [Parameter]
    public EventCallback<WorkItemDto> OnSave { get; set; }

    public bool IsVisible { get; private set; }

    public void Show() => IsVisible = true;
    public void Close() => IsVisible = false;

    private async Task SaveWorkItem()
    {
        await OnSave.InvokeAsync(WorkItem);
        Close();
    }
}
