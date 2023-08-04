using HadisIelts.Shared.Requests.Admin;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Admininstrator
{
    public class GetUsersRolesHandler
        : IRequestHandler<GetUsersRolesRequest, GetUsersRolesRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public GetUsersRolesHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GetUsersRolesRequest.Response> Handle(GetUsersRolesRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync(GetUsersRolesRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetUsersRolesRequest.Response>();
                return result;
            }
            return null;
        }
    }
}
