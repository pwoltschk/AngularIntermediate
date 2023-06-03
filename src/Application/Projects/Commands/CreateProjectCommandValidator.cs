using Application.Projects.Requests;


namespace Application.Projects.Commands;
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private const string UniqueTitleErrorMessage = "The project title must be unique.";
    private const string UniqueTitleErrorCode = "ERR_UNIQUE_TITLE";

    public CreateProjectCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Project)
            .SetValidator(new CreateProjectRequestValidator());

        RuleFor(command => command.Project.Title)
            .MustAsync(IsTitleUnique)
            .WithMessage(UniqueTitleErrorMessage)
            .WithErrorCode(UniqueTitleErrorCode);
    }

    private Task<bool> IsTitleUnique(string title, CancellationToken cancellationToken) => _dbContext.Projects
            .AllAsync(project => project.Title != title, cancellationToken);

}

