using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record RegisterAccountRequest(string Email, string Password, string FirstName, string LastName, DateTime? Birthday)
        : IRequest<RegisterAccountRequest.Response>
    {
        public const string EndpointUri = "/api/account/register";
        public record Response(bool registerSuccess);
    }
    public class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
    {
        public RegisterAccountRequestValidator()
        {
            //email rules
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            //password rules
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
        }
    }
}
