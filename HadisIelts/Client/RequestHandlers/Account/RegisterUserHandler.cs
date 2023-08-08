using HadisIelts.Client.RequestHandlers.Account.Services;
using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterAccountRequest, RegisterAccountRequest.Response>
    {
        private readonly HttpClient _httpClient;
        private readonly IPasswordService _passwordService;
        public RegisterUserHandler(IHttpClientFactory httpClientFactory, IPasswordService passwordService)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
            _passwordService = passwordService;
        }

        public async Task<RegisterAccountRequest.Response> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
        {
            request.Request.Password = _passwordService.HashPassword(request.Request.Password);
            var response = await _httpClient.PostAsJsonAsync
                (RegisterAccountRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>
                    (cancellationToken: cancellationToken);
                return new RegisterAccountRequest.Response(result);
            }
            return new RegisterAccountRequest.Response(false);
        }
    }
}
