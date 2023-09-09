using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class ProcessWritingCorrectionFilesHandler
        : IRequestHandler<ProcessWritingFilesRequest, ProcessWritingFilesRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public ProcessWritingCorrectionFilesHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProcessWritingFilesRequest.Response> Handle(ProcessWritingFilesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync
                    (ProcessWritingFilesRequest.EndPointUri, request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ProcessWritingFilesRequest.Response>();
                    return result;
                }
                return new ProcessWritingFilesRequest.Response(new WritingCorrectionPackageSharedModel
                {
                    ProcessedWritingFiles = null,

                }, Message: "Bad Request!");
            }
            catch (Exception e)
            {
                return new ProcessWritingFilesRequest.Response(new WritingCorrectionPackageSharedModel
                {
                    ProcessedWritingFiles = null
                }
                , Message: "failed");
            }
        }
    }
}
