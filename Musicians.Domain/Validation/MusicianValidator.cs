using FluentValidation;
using Musicians.Domain.Entities;

namespace Musicians.Domain.Validation;

public class MusicianValidator : AbstractValidator<Musician>
{
    public MusicianValidator()
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage("Musician Name is required")
            .MaximumLength(80).WithMessage("Musician name too long. Max is 80 chars");

        RuleFor(m => m.Genre).NotEmpty().WithMessage("Genre is required");

        RuleFor(m => m.PerformAs).NotEmpty().WithMessage("Musician performance is required");
    }

}