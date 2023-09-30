using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Account;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class GetUserInfoHandler : BaseMediatorRequestHandler
        <GetUserInformationRequest, GetUserInformationRequest.Response>
    {
        public GetUserInfoHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(GetUserInformationRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
