using HadisIelts.Shared.Requests.Account;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class GetUserInfoHandler : BaseMediatorRequestHandler
        <GetUserInformationRequest, GetUserInformationRequest.Response>
    {
        public GetUserInfoHandler(HttpClient httpClient)
            : base(httpClient, GetUserInformationRequest.EndPointUri)
        {
        }
        public override GetUserInformationRequest.Response HandleError(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                default:
                    return new GetUserInformationRequest.Response(null);
            }

        }
    }
}
