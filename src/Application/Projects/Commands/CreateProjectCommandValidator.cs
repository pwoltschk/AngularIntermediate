using Application.Projects.Requests;
using Domain.Primitives;

namespace Application.Projects.Commands;
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    private readonly IRepository<Project> _repository;
    private const string UniqueTitleErrorMessage = "The project title must be unique.";
    private const string UniqueTitleErrorCode = "ERR_UNIQUE_TITLE";

    public CreateProjectCommandValidator(IRepository<Project> repository)
    {
        _repository = repository;

        RuleFor(command => command.Project)
            .SetValidator(new CreateProjectRequestValidator());

        RuleFor(command => command.Project.Title)
            .MustAsync(IsTitleUnique)
            .WithMessage(UniqueTitleErrorMessage)
            .WithErrorCode(UniqueTitleErrorCode);
    }

    private async Task<bool> IsTitleUnique(string title, CancellationToken cancellationToken) => (await _repository.GetAllAsync())
            .All(project => project.Title != title);

}

