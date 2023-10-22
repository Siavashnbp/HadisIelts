using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class ForgotPasswordRequestHandler :
        IRequestHandler<ForgotPasswordRequest>
    {
        private readonly HttpClient _httpClient;
        public ForgotPasswordRequestHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
        }
        public async Task Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            await _httpClient.PostAsJsonAsync(ForgotPasswordRequest.EndpointUri, request, cancellationToken);

        }
    }
}
