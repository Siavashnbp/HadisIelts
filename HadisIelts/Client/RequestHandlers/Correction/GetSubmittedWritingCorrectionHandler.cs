using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class GetSubmittedWritingCorrectionHandler : BaseMediatorRequestHandler
        <GetSubmittedWritingCorrectionFilesRequest, GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public GetSubmittedWritingCorrectionHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(GetSubmittedWritingCorrectionFilesRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
