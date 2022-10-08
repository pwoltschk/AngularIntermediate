using Application.Projects.Requests;

namespace Application.Projects.Commands;
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.Project).SetValidator(new CreateProjectRequestValidator());
    }
}