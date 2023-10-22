using FluentValidation;

namespace HadisIelts.Client.Features.Account.Models
{
    public class ChangePasswordModel
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(x => x.NewPassword).NotEmpty()
                .WithMessage("Password cannot be empty");
            RuleFor(x => x.NewPassword).Matches(@"[a-z]")
                .WithMessage("Password must contain at least a lower case character");
            RuleFor(x => x.NewPassword).Matches(@"[A-Z]")
                .WithMessage("Password must contain at least a upper case character");
            RuleFor(x => x.NewPassword).Matches(@"[0-9]")
                .WithMessage("Password must contain at least a number");
            RuleFor(x => x.NewPassword).MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");
            RuleFor(x => x.ConfirmNewPassword).NotEmpty()
                .WithMessage("Please confirm your password");
            RuleFor(x => x.ConfirmNewPassword).Matches(x => x.NewPassword)
                .WithMessage("Passwords do not match");
        }
    }
}
