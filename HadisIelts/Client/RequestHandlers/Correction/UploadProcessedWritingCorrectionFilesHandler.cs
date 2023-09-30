using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class UploadProcessedWritingCorrectionFilesHandler : BaseMediatorRequestHandler
        <UploadProcessedWritingFilesRequest, UploadProcessedWritingFilesRequest.Response>
    {
        public UploadProcessedWritingCorrectionFilesHandler() : base(UploadProcessedWritingFilesRequest.EndpointUri)
        {
        }
    }
}
