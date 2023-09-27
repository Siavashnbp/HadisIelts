using HadisIelts.Shared.Requests.User;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.User
{
    public class UploadWritingCorrectionAllowanceRequestHandler : IRequestHandler
        <UploadWritingCorrectionAllowanceRequest, UploadWritingCorrectionAllowanceRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public UploadWritingCorrectionAllowanceRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UploadWritingCorrectionAllowanceRequest.Response> Handle(UploadWritingCorrectionAllowanceRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(UploadWritingCorrectionAllowanceRequest.EndpointUri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UploadWritingCorrectionAllowanceRequest.Response>();
                return result!;
            }
            return null!;
        }
    }
}
