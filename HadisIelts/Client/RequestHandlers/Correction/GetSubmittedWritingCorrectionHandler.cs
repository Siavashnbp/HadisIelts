using HadisIelts.Shared.Requests.Correction;
using HadisIelts.Shared.Requests.Payment;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class GetSubmittedWritingCorrectionHandler : IRequestHandler
        <GetSubmittedWritingCorrectionFilesRequest, GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public GetSubmittedWritingCorrectionHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GetSubmittedWritingCorrectionFilesRequest.Response> Handle(GetSubmittedWritingCorrectionFilesRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (GetSubmittedWritingCorrectionFilesRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetSubmittedWritingCorrectionFilesRequest.Response>();
                return result;
            }
            return new GetSubmittedWritingCorrectionFilesRequest.Response(new CalculatedWritingCorrectionPayment
            {
                ProcessedFiles = new List<ProcessedWritingFile>(),
                Message = "There was a problem with the request"
            });
        }
    }
}
