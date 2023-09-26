using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record GetUserInformationRequest(string UserId) :
        IRequest<GetUserInformationRequest.Response>
    {
        public const string EndPointUri = "/api/user/getUserInfo";
        public record Response(UserInformationSharedModel userInformation);
    }
    public class GetUserInformationRequestValidator : AbstractValidator<GetUserInformationRequest>
    {
        public GetUserInformationRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
        }
    }
}
