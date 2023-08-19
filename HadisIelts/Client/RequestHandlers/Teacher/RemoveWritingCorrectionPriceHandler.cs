using HadisIelts.Shared.Requests.Teacher;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class RemoveWritingCorrectionPriceHandler : IRequestHandler
        <RemoveWritingCorrectionPriceRequest, RemoveWritingCorrectionPriceRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public RemoveWritingCorrectionPriceHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<RemoveWritingCorrectionPriceRequest.Response> Handle(RemoveWritingCorrectionPriceRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (RemoveWritingCorrectionPriceRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RemoveWritingCorrectionPriceRequest.Response>();
                return result;
            }
            return new RemoveWritingCorrectionPriceRequest.Response(false);
        }
    }
}
