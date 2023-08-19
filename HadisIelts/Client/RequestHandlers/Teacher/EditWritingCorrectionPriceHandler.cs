using HadisIelts.Shared.Requests.Teacher;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class EditWritingCorrectionPriceHandler : IRequestHandler
        <EditWritingCorrectionPriceRequest, EditWritingCorrectionPriceRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public EditWritingCorrectionPriceHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<EditWritingCorrectionPriceRequest.Response> Handle(EditWritingCorrectionPriceRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (EditWritingCorrectionPriceRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<EditWritingCorrectionPriceRequest.Response>();
                return result;
            }
            return null;
        }
    }
}
