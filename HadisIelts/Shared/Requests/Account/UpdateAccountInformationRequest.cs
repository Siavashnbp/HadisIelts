using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record UpdateAccountInformationRequest(UserInformationSharedModel UserInformation)
        : IRequest<UpdateAccountInformationRequest.Response>
    {
        public const string EndpointUri = "/api/account/updateInformation";
        public record Response(UserInformationSharedModel UpdatedUserInformation);
    }
    public class UpdateAccountInformationRequestValidator
        : AbstractValidator<UpdateAccountInformationRequest>
    {
        public UpdateAccountInformationRequestValidator()
        {
            RuleFor(x => x.UserInformation.Username).NotNull().NotEmpty();
            RuleFor(x => x.UserInformation.Email).NotNull().NotEmpty();
            RuleFor(x => x.UserInformation.FirstName).NotNull().NotEmpty()
                .WithMessage("Name field is required");
            RuleFor(x => x.UserInformation.LastName).NotNull().NotEmpty()
                .WithMessage("Name field is required");
        }
    }
}
