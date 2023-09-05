using FluentValidation;
using Musicians.Domain.Entities;

namespace Musicians.Domain.Validation;

public class UserValidator :AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email");

        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password too short. Minimun is 8 characters");

    }
}






