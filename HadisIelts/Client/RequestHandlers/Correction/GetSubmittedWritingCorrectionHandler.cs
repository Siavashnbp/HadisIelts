using HadisIelts.Shared.Requests.Correction;
using System.Net;

namespace HadisIelts.Client.RequestHandlers.Correction
{
    public class GetSubmittedWritingCorrectionHandler : BaseMediatorRequestHandler
        <GetSubmittedWritingCorrectionFilesRequest, GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public GetSubmittedWritingCorrectionHandler(HttpClient httpClient)
            : base(httpClient, GetSubmittedWritingCorrectionFilesRequest.EndpointUri)
        {
        }
        public override GetSubmittedWritingCorrectionFilesRequest.Response HandleError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new GetSubmittedWritingCorrectionFilesRequest.Response(null!, HttpStatusCode.Unauthorized);
            }
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return new GetSubmittedWritingCorrectionFilesRequest.Response(null!, HttpStatusCode.Conflict);
            }
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new GetSubmittedWritingCorrectionFilesRequest.Response(null!, HttpStatusCode.NoContent);
            }
            return null!;
        }
    }
}
