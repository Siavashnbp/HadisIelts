using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class ChangePasswordRequestHandler : IRequestHandler
        <ChangePasswordRequest, ChangePasswordRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public ChangePasswordRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ChangePasswordRequest.Response> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync(ChangePasswordRequest.EndpointUri, request, cancellationToken);
            return new ChangePasswordRequest.Response(response.IsSuccessStatusCode);
        }
    }
}
