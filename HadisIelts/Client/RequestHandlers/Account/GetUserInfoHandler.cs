using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class GetUserInfoHandler
        : IRequestHandler<GetUserInformationRequest, GetUserInformationRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public GetUserInfoHandler(IHttpClientFactory httpClientFactory, HttpClient httpClient)
        {
            //_httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
            _httpClient = httpClient;
        }
        public async Task<GetUserInformationRequest.Response> Handle(GetUserInformationRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (GetUserInformationRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<UserInformation>();
                if (result.Result is not null)
                {
                    return new GetUserInformationRequest.Response(result.Result);
                }
                return new GetUserInformationRequest.Response(new UserInformation
                {
                    Username = "Not Found",
                    Email = "Not Found",
                    FirstName = "Not Found",
                    LastName = "Not Found",
                    Skype = "Not Found"
                });
            }
            return null;

        }
    }
}
