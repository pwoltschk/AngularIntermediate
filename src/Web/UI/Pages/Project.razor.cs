using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class Project
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    private bool _showEditProjectDialog = false;
    private ProjectDto _editProject = new();

    private bool IsSelected(ProjectDto list)
    {
        return State.SelectedList!.Id == list.Id;
    }

    private void SelectList(ProjectDto list)
    {
        if (IsSelected(list)) return;

        State.SelectedList = list;
    }

    private void ShowEditProjectDialog(ProjectDto project)
    {
        _editProject = project;
        _showEditProjectDialog = true;
    }

    public async Task UpdateProject()
    {
        await State.ProjectsClient.PutProjectAsync(_editProject.Id, new UpdateProjectRequest
        {
            Title = _editProject.Title,
            Id = _editProject.Id
        });
        _showEditProjectDialog = false;
    }

}
