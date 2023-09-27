using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class UploadProcessedWritingCorrectionFilesHandler : BaseMediatorRequestHandler
        <UploadProcessedWritingFilesRequest, UploadProcessedWritingFilesRequest.Response>
    {
        public UploadProcessedWritingCorrectionFilesHandler(HttpClient httpClient)
            : base(httpClient, UploadProcessedWritingFilesRequest.EndpointUri)
        {
        }
    }
}
