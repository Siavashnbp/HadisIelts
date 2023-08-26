using HadisIelts.Shared.Requests.Correction;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class UploadProcessedWritingCorrectionFiles : IRequestHandler
        <UploadProcessedWritingFilesRequest, UploadProcessedWritingFilesRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public UploadProcessedWritingCorrectionFiles(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UploadProcessedWritingFilesRequest.Response> Handle(UploadProcessedWritingFilesRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (UploadProcessedWritingFilesRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UploadProcessedWritingFilesRequest.Response>();
                return result;
            }
            return null;
        }
    }
}
