using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword) : IRequest<ChangePasswordRequest.Response>
    {
        public const string EndpointUri = "/api/account/changePassword";
        public record Response(bool Succeeded);
    }
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
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
        }
    }
}
