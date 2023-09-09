using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Account;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class GetUserInfoHandler
        : IRequestHandler<GetUserInformationRequest, GetUserInformationRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public GetUserInfoHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GetUserInformationRequest.Response> Handle(GetUserInformationRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (GetUserInformationRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<GetUserInformationRequest.Response>();
                if (result.Result is not null)
                {
                    return new GetUserInformationRequest.Response(result.Result.userInformation);
                }
                return new GetUserInformationRequest.Response(new UserInformationSharedModel(username: null, email: null)
                {
                    FirstName = "Not Found",
                    LastName = "Not Found",
                    Skype = "Not Found"
                });
            }
            return null;

        }
    }
}
