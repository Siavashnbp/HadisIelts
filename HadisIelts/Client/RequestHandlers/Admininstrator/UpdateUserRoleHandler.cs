using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Administrator;

namespace HadisIelts.Client.RequestHandlers.Admininstrator
{
    public class UpdateUserRoleHandler : BaseMediatorRequestHandler
        <UpdateUserRoleRequest, UpdateUserRoleRequest.Response>

    {
        public UpdateUserRoleHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(UpdateUserRoleRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
