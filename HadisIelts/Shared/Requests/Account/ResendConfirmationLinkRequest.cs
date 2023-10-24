using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record ResendConfirmationLinkRequest(string Email) : IRequest<ResendConfirmationLinkRequest.Response>
    {
        public const string EndpointUri = "/api/account/resendConfirmationLink";
        public record Response(bool Succeeded);
    }
    public class ResendConfirmationLinkRequestValidator : AbstractValidator<ResendConfirmationLinkRequest>
    {
        public ResendConfirmationLinkRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
