using Microsoft.AspNetCore.Components;

namespace UI.Components.WorkItems;

public partial class WorkItem
{
    [Parameter]
    public WorkItemDto Item { get; set; } = null!;

    [Parameter]
    public EventCallback<int> OnDelete { get; set; }

    [Parameter]
    public EventCallback<WorkItemDto> OnEdit { get; set; }

    private string GetBorderColor() => Item.Stage switch
    {
        0 => "black",
        1 => "orange",
        2 => "green",
        _ => "gray"
    };

    private string GetPriorityName(int priority) => priority switch
    {
        0 => "Low",
        1 => "Medium",
        2 => "High",
        _ => "Unknown"
    };
}