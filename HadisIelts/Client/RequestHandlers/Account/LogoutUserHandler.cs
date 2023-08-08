using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class LogoutUserHandler :
        IRequestHandler<AccountLogoutRequest>
    {
        private readonly HttpClient _httpClient;
        public LogoutUserHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Handle(AccountLogoutRequest request, CancellationToken cancellationToken)
        {
            await _httpClient.PostAsJsonAsync
                (AccountLogoutRequest.EndPointUri, request, cancellationToken);
        }
    }
}
