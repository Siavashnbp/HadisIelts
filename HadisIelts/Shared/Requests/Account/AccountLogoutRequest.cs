using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record AccountLogoutRequest() : IRequest

    {
        public const string EndPointUri = "/api/account/logout";
    }

}
