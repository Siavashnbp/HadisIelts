using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class LoginUserHandler
        : IRequestHandler<AccountLoginRequest, AccountLoginRequest.Response>
    {
        private readonly HttpClient _httpClient;
        private readonly IPasswordService _passwordService;
        public LoginUserHandler(IHttpClientFactory httpClientFactory, IPasswordService passwordService)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
            _passwordService = passwordService;
        }
        public async Task<AccountLoginRequest.Response> Handle(AccountLoginRequest request, CancellationToken cancellationToken)
        {
            request.Request.Password = _passwordService.HashPassword(request.Request.Password);
            var response = await _httpClient.PostAsJsonAsync
                (AccountLoginRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new AccountLoginRequest.Response(true);
            }
            return new AccountLoginRequest.Response(false);
        }
    }
}
