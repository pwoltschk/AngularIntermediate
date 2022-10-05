namespace Application.Projects.Requests;
public class CreateProjectRequestValidator
    : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(150)
            .NotEmpty();
    }
}

