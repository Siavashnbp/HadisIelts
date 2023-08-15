using HadisIelts.Shared.Requests.Teacher;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class AddWritingCorrectionServicePriceHandler : IRequestHandler
        <AddWritingCorrectionPriceRequest, AddWritingCorrectionPriceRequest.Response>
    {
        private HttpClient _httpClient;
        public AddWritingCorrectionServicePriceHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AddWritingCorrectionPriceRequest.Response> Handle(AddWritingCorrectionPriceRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (AddWritingCorrectionPriceRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AddWritingCorrectionPriceRequest.Response>();
                return result;
            }
            return null;
        }
    }
}
