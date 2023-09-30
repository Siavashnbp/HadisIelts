using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Admin;

namespace HadisIelts.Client.RequestHandlers.Admininstrator
{
    public class GetUsersRolesHandler : BaseMediatorRequestHandler
        <GetUsersRolesRequest, GetUsersRolesRequest.Response>
    {
        public GetUsersRolesHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(GetUsersRolesRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
