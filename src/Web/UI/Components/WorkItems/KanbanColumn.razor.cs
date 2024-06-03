using Microsoft.AspNetCore.Components;

namespace UI.Components.WorkItems;
public partial class KanbanColumn
{
    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public List<WorkItemDto> Items { get; set; } = [];

    [Parameter]
    public EventCallback<WorkItemDto> OnItemDrop { get; set; }

    [Parameter]
    public EventCallback<int> OnDelete { get; set; }

    [Parameter]
    public EventCallback<WorkItemDto> OnEdit { get; set; }
}
