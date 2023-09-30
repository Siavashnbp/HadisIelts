using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record AccountLoginRequest(LoginSharedModel LoginRequest) :
        IRequest<AccountLoginRequest.Response>
    {
        public const string EndPointUri = "/api/account/login";
        public record Response(bool LoginSuccess) : ServerResponse;
    }
    public class AccountLoginRequestValidator : AbstractValidator<AccountLoginRequest>
    {
        public AccountLoginRequestValidator()
        {
            RuleFor(x => x.LoginRequest).SetValidator(new LoginModelValidator());
        }
    }
    public class LoginModelValidator : AbstractValidator<LoginSharedModel>
    {
        public LoginModelValidator()
        {
            //Email rules
            RuleFor(x => x.Email).NotNull().NotEmpty()
                .WithMessage("Email field cannot be empty");
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Email is not in correct format");
            //Password rules
            RuleFor(x => x.Password).NotNull().NotEmpty()
                .WithMessage("Passwrod cannot be empty");
        }
    }
}
