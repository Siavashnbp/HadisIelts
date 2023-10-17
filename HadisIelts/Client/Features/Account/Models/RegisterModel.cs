using FluentValidation;

namespace HadisIelts.Client.Features.Account.Models
{
    public class RegisterModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("This in not a valid email format");
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Please enter your email address");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password cannot be empty");
            RuleFor(x => x.Password).Matches(@"[a-z]")
                .WithMessage("Password must contain at least a lower case character");
            RuleFor(x => x.Password).Matches(@"[A-Z]")
                .WithMessage("Password must contain at least a upper case character");
            RuleFor(x => x.Password).Matches(@"[0-9]")
                .WithMessage("Password must contain at least a number");
            RuleFor(x => x.Password).MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");
            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .WithMessage("Please confirm your password");
            RuleFor(x => x.ConfirmPassword).Matches(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
