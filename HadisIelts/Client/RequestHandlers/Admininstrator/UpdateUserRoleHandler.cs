using HadisIelts.Shared.Requests.Administrator;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Admininstrator
{
    public class UpdateUserRoleHandler
        : IRequestHandler<UpdateUserRoleRequest, UpdateUserRoleRequest.Response>

    {
        private readonly HttpClient _httpClient;
        public UpdateUserRoleHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UpdateUserRoleRequest.Response> Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync
                    (UpdateUserRoleRequest.EndPointUri, request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UpdateUserRoleRequest.Response>();
                    return result;
                }
                return new UpdateUserRoleRequest.Response(
                    UserRoles: default,
                    Message: "Bad request!"
                );
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
