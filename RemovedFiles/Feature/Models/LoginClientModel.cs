using FluentValidation;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Account;

namespace HadisIelts.Client.Features.Account.Models
{
    public class LoginClientModel
    {
        public LoginSharedModel LoginModel { get; set; }
        public string? LoginMessage { get; set; }
        public LoginClientModel()
        {
            LoginModel = new LoginSharedModel();
        }
    }
    public class LoginClientModelValidator : AbstractValidator<LoginClientModel>
    {
        public LoginClientModelValidator()
        {
            RuleFor(x => x.LoginModel).SetValidator(new LoginModelValidator());
        }
    }
}
