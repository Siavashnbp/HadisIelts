using HadisIelts.Shared.Requests.Correction;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class SubmitWritingCorrectionHandler
        : IRequestHandler<SubmitWritingCorrectionRequest, SubmitWritingCorrectionRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public SubmitWritingCorrectionHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SubmitWritingCorrectionRequest.Response> Handle(SubmitWritingCorrectionRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (SubmitWritingCorrectionRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var submittedRequestId = await response.Content.ReadFromJsonAsync<int>
                    (cancellationToken: cancellationToken);
                return new SubmitWritingCorrectionRequest.Response(submittedRequestId);
            }
            else
            {
                return new SubmitWritingCorrectionRequest.Response(-1);
            }
        }
    }
}
