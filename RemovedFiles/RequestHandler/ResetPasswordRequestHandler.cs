using HadisIelts.Client.Services.Account.Services;
using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class ResetPasswordRequestHandler :
        IRequestHandler<ResetPasswordRequest, ResetPasswordRequest.Response>
    {
        private readonly HttpClient _httpClient;
        private readonly IPasswordService _passwordService;
        public ResetPasswordRequestHandler(IHttpClientFactory httpClientFactory,
            IPasswordService passwordService)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
            _passwordService = passwordService;
        }
        public async Task<ResetPasswordRequest.Response> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordService.HashPassword(request.Password);
            var processedRequest = new ResetPasswordRequest(hashedPassword, request.Token, request.UserId);
            var response = await _httpClient.PostAsJsonAsync(ResetPasswordRequest.EndpointUri, processedRequest, cancellationToken);
            return new ResetPasswordRequest.Response(response.IsSuccessStatusCode);
        }
    }
}
