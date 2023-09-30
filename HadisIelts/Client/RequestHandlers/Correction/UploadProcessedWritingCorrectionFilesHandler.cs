using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class UploadProcessedWritingCorrectionFilesHandler : BaseMediatorRequestHandler
        <UploadProcessedWritingFilesRequest, UploadProcessedWritingFilesRequest.Response>
    {
        public UploadProcessedWritingCorrectionFilesHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(UploadProcessedWritingFilesRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
