using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record SubmitRegisterUserRequest(RegiterUserRequest Request)
        : IRequest<SubmitRegisterUserRequest.Response>
    {
        public const string EndpointUri = "/api/account/register";
        public record Response(bool IsSuccessful);
    }
    public class RegiterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
