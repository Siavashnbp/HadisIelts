using HadisIelts.Shared.Requests.Administrator;

namespace HadisIelts.Client.RequestHandlers.Admininstrator
{
    public class UpdateUserRoleHandler : BaseMediatorRequestHandler
        <UpdateUserRoleRequest, UpdateUserRoleRequest.Response>

    {
        public UpdateUserRoleHandler(HttpClient httpClient)
            : base(httpClient, UpdateUserRoleRequest.EndPointUri)
        {
        }
    }
}
