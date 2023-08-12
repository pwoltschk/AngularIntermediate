using Microsoft.AspNetCore.Components;

namespace UI.Components;
public partial class KanbanColumn
{
    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public List<WorkItemDto> Items { get; set; } = new();

    [Parameter]
    public EventCallback<WorkItemDto> OnItemDrop { get; set; }

    [Parameter]
    public EventCallback<int> OnDelete { get; set; }

    [Parameter]
    public EventCallback<WorkItemDto> OnEdit { get; set; }
}
