using HadisIelts.Shared.Requests.Correction;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.General
{
    public class GetWritingTypesHandler : IRequestHandler
        <GetWritingTypesRequest, GetWritingTypesRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public GetWritingTypesHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
        }
        public async Task<GetWritingTypesRequest.Response> Handle(GetWritingTypesRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(GetWritingTypesRequest.EndPointUri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetWritingTypesRequest.Response>();
                return result;
            }
            return null;
        }
    }
}
