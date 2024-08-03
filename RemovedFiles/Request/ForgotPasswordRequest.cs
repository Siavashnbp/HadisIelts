using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record ForgotPasswordRequest(string Email) : IRequest
    {
        public const string EndpointUri = "/api/account/forgotPassword";
    }
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty()
                .WithMessage("Email field cannot be empty");
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Email is not in correct format");
        }
    }
}
