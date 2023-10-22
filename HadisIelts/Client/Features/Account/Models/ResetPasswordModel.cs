using FluentValidation;

namespace HadisIelts.Client.Features.Account.Models
{
    public class ResetPasswordModel
    {
        public string? Email { get; set; }
    }
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty()
                .WithMessage("Email field cannot be empty");
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Email is not in correct format");
        }
    }
}
