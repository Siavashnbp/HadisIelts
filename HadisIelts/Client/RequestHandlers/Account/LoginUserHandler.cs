using HadisIelts.Client.RequestHandlers.Account.Services;
using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class LoginUserHandler
        : IRequestHandler<AccountLoginRequest, AccountLoginRequest.Response?>
    {
        private readonly HttpClient _httpClient;
        private readonly IPasswordService _passwordService;
        public LoginUserHandler(IHttpClientFactory httpClientFactory, IPasswordService passwordService)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
            _passwordService = passwordService;
        }
        public async Task<AccountLoginRequest.Response?> Handle(AccountLoginRequest request, CancellationToken cancellationToken)
        {
            request.LoginRequest.Password = _passwordService.HashPassword(request.LoginRequest.Password);
            var response = await _httpClient.PostAsJsonAsync
                (AccountLoginRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AccountLoginRequest.Response>();
                return result;
            }
            return new AccountLoginRequest.Response(
                LoginSuccess: false,
                Message: "Bad Request!"
            );
        }
    }
}
