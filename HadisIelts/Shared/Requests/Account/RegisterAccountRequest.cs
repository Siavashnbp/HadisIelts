using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record RegisterAccountRequest(RegiterRequest Request)
        : IRequest<RegisterAccountRequest.Response>
    {
        public const string EndpointUri = "/api/account/register";
        public record Response(bool registerSuccess);
    }
    public class RegiterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
