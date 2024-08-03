using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record ResetPasswordRequest(string Password, string Token, string UserId)
        : IRequest<ResetPasswordRequest.Response>
    {
        public const string EndpointUri = "/api/account/resetPassword";
        public record Response(bool Succeeded);
    }
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
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
            RuleFor(x => x.Token).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
        }
    }
}
