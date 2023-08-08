using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record AccountLoginRequest(LoginRequest Request) :
        IRequest<AccountLoginRequest.Response>
    {
        public const string EndPointUri = "/api/account/login";
        public record Response(LoginResponse LoginResponse);
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool KeepSignedIn { get; set; }
    }
    public class LoginResponse
    {
        public bool LoginSucess { get; set; }
        public string Message { get; set; }
    }
}
