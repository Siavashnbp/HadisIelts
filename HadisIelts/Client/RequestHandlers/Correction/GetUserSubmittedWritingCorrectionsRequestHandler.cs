using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class GetUserSubmittedWritingCorrectionsRequestHandler : BaseMediatorRequestHandler
        <GetUserSubmittedWritingCorrectionRequest, GetUserSubmittedWritingCorrectionRequest.Response>
    {
        public GetUserSubmittedWritingCorrectionsRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(GetUserSubmittedWritingCorrectionRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
