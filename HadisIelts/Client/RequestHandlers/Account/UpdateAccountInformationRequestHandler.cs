using HadisIelts.Shared.Requests.Account;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class UpdateAccountInformationRequestHandler : BaseMediatorRequestHandler
        <UpdateAccountInformationRequest, UpdateAccountInformationRequest.Response>
    {
        public UpdateAccountInformationRequestHandler(HttpClient httpClient)
            : base(httpClient, UpdateAccountInformationRequest.EndpointUri)
        {
        }
    }
}
