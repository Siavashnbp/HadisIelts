using HadisIelts.Client.Services.Account.Services;
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
            var hashedPassword = _passwordService.HashPassword(request.Password);
            var updatedRequest = new RegisterAccountRequest(request.Email, hashedPassword, request.FirstName, request.LastName, request.Birthday);
            var response = await _httpClient.PostAsJsonAsync
                (RegisterAccountRequest.EndpointUri, updatedRequest, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RegisterAccountRequest.Response>();
                return result!;
            }
            return null!;
        }
    }
}
