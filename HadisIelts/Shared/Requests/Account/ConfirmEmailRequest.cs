using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record ConfirmEmailRequest(string UserId, string Token) : IRequest<ConfirmEmailRequest.Response>
    {
        public const string EndpointUri = "/api/account/confirmEmail";
        public record Response(bool Succeeded);
    }
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Token).NotNull().NotEmpty();
        }
    }
}
