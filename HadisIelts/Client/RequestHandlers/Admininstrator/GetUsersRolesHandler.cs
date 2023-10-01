using HadisIelts.Shared.Requests.Admin;

namespace HadisIelts.Client.RequestHandlers.Admininstrator
{
    public class GetUsersRolesHandler : BaseMediatorRequestHandler
        <GetUsersRolesRequest, GetUsersRolesRequest.Response>
    {
        public GetUsersRolesHandler() : base(GetUsersRolesRequest.EndPointUri)
        {
        }
    }
}
