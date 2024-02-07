using FluentValidation;

namespace UI.Validators;

public class WorkItemDtoValidator : AbstractValidator<WorkItemDto>
{
    public WorkItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}