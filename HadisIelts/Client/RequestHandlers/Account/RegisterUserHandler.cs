using HadisIelts.Shared.Requests.Account;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class RegisterUserHandler
        : IRequestHandler<SubmitRegisterUserRequest, SubmitRegisterUserRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public RegisterUserHandler(IHttpClientFactory httpClientFactory)
        {

            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
        }

        public async Task<SubmitRegisterUserRequest.Response> Handle(SubmitRegisterUserRequest request, CancellationToken cancellationToken)
        {
            string hashedPassword = HashPassword(request.Request.Password);
            request.Request.Password = hashedPassword;
            var response = await _httpClient.PostAsJsonAsync
                (SubmitRegisterUserRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>
                    (cancellationToken: cancellationToken);
                return new SubmitRegisterUserRequest.Response(result);
            }
            return new SubmitRegisterUserRequest.Response(false);
        }
        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
            return hashedPassword;
        }
    }
}
