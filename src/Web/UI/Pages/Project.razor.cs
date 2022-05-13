using Microsoft.AspNetCore.Components;


namespace UI.Pages;

public partial class Project
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    private bool _showCreateProjectDialog = false;
    private bool _showEditProjectDialog = false;
    private ProjectDto _createProject = new();
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

    private void ShowCreateProjectDialog()
    {
        _showCreateProjectDialog = true;
    }

    private void ShowEditProjectDialog(ProjectDto project)
    {
        _editProject = project;
        _showEditProjectDialog = true;
    }

    public async Task CreateProject()
    {
        var projectId = await State.ProjectsClient.PostProjectAsync(new CreateProjectRequest
        {
            Title = _createProject.Title
        });
        var project = new ProjectDto { Id = projectId, Title = _createProject.Title };
        State.Model!.Projects.Add(project);
        _createProject = new ProjectDto();
        _showCreateProjectDialog = false;
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

