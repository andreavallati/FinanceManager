using FinanceManager.WPF.Presentation.ViewModels;
using FluentValidation;

namespace FinanceManager.WPF.Application.Validation
{
    public class UserValidator : AbstractValidator<RegisterViewModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(150).WithMessage("Email must not exceed 150 characters.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.SelectedUserRole)
                .IsInEnum().WithMessage("A valid role must be selected.");
        }
    }
}
