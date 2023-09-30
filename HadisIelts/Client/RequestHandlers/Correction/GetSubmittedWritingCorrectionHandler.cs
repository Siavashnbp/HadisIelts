using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class GetSubmittedWritingCorrectionHandler : BaseMediatorRequestHandler
        <GetSubmittedWritingCorrectionFilesRequest, GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public GetSubmittedWritingCorrectionHandler() : base(GetSubmittedWritingCorrectionFilesRequest.EndpointUri)
        {
        }
    }
}
