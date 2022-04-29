using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class WorkItems
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;
}