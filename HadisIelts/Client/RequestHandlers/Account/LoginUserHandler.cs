using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Account;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class LoginUserHandler : BaseMediatorRequestHandler
        <AccountLoginRequest, AccountLoginRequest.Response>
    {
        public LoginUserHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(AccountLoginRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
