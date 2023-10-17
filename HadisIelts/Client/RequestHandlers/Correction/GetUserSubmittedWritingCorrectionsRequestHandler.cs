using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class GetUserSubmittedWritingCorrectionsRequestHandler : BaseMediatorRequestHandler
        <GetUserSubmittedWritingCorrectionRequest, GetUserSubmittedWritingCorrectionRequest.Response>
    {
        public GetUserSubmittedWritingCorrectionsRequestHandler(HttpClient httpClient)
            : base(httpClient, GetUserSubmittedWritingCorrectionRequest.EndpointUri)
        {
        }
        public override GetUserSubmittedWritingCorrectionRequest.Response HandleError(HttpResponseMessage response)
        {
            return new GetUserSubmittedWritingCorrectionRequest.Response(null!, response.StatusCode);
        }
    }
}
