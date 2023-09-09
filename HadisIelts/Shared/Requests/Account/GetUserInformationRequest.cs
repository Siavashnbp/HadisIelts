using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record GetUserInformationRequest(string UserID, string RequestedUserID) :
        IRequest<GetUserInformationRequest.Response>
    {
        public const string EndPointUri = "/api/getUserInfo";
        public record Response(UserInformationSharedModel userInformation);
    }
    public class GetUserInformationRequestValidator : AbstractValidator<GetUserInformationRequest>
    {
        public GetUserInformationRequestValidator()
        {
            RuleFor(x => x.UserID).NotEmpty().NotNull();
            RuleFor(x => x.RequestedUserID).NotEmpty().NotNull();
        }
    }
}
