using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class ResendConfirmationLinkRequestHandler : IRequestHandler
        <ResendConfirmationLinkRequest, ResendConfirmationLinkRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public ResendConfirmationLinkRequestHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
        }
        public async Task<ResendConfirmationLinkRequest.Response?> Handle(ResendConfirmationLinkRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (ResendConfirmationLinkRequest.EndpointUri, request, cancellationToken);
            return new ResendConfirmationLinkRequest.Response(response.IsSuccessStatusCode);
        }
    }
}
