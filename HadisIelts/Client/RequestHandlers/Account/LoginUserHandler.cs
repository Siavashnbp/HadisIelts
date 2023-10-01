using HadisIelts.Shared.Requests.Account;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class LoginUserHandler : BaseMediatorRequestHandler
        <AccountLoginRequest, AccountLoginRequest.Response>
    {
        public LoginUserHandler() : base(AccountLoginRequest.EndPointUri)
        {
        }
    }
}
