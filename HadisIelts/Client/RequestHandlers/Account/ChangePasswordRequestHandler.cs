using HadisIelts.Client.Services.Account.Services;
using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class ChangePasswordRequestHandler : IRequestHandler
        <ChangePasswordRequest, ChangePasswordRequest.Response>
    {
        private readonly IPasswordService _passwordServices;
        private readonly HttpClient _httpClient;
        public ChangePasswordRequestHandler(IPasswordService passwordService, HttpClient httpClient)
        {
            _passwordServices = passwordService;
            _httpClient = httpClient;
        }
        public async Task<ChangePasswordRequest.Response> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var hashedCurrentPassword = _passwordServices.HashPassword(request.CurrentPassword);
            var hashedNewPassword = _passwordServices.HashPassword(request.NewPassword);
            var newRequest = new ChangePasswordRequest(hashedCurrentPassword, hashedNewPassword);
            var response = await _httpClient.PostAsJsonAsync(ChangePasswordRequest.EndpointUri, newRequest, cancellationToken);
            return new ChangePasswordRequest.Response(response.IsSuccessStatusCode);
        }
    }
}
