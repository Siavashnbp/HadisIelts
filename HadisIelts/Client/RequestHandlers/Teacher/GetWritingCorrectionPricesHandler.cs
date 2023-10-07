using HadisIelts.Shared.Requests.Correction;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class GetWritingCorrectionPricesHandler : IRequestHandler
        <GetWritingCorrectionPricesRequest, GetWritingCorrectionPricesRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public GetWritingCorrectionPricesHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HadisIelts.AnonymousAPI");
        }
        public async Task<GetWritingCorrectionPricesRequest.Response> Handle(GetWritingCorrectionPricesRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(GetWritingCorrectionPricesRequest.EndPointUri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetWritingCorrectionPricesRequest.Response>();
                return result!;
            }
            return null!;
        }
    }
}
