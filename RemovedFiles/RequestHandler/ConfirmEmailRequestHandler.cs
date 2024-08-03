using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class ConfirmEmailRequestHandler : IRequestHandler
        <ConfirmEmailRequest, ConfirmEmailRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public ConfirmEmailRequestHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
        }
        public async Task<ConfirmEmailRequest.Response?> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (ConfirmEmailRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new ConfirmEmailRequest.Response(true);
            }
            return new ConfirmEmailRequest.Response(false);
        }
    }
}
