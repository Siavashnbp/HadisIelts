using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class ChangeWritingGroupCorrectionStatusRequestHandler : BaseMediatorRequestHandler
        <ChangeWritingGroupCorrectionStatusRequest, ChangeWritingGroupCorrectionStatusRequest.Response>
    {
        public ChangeWritingGroupCorrectionStatusRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(ChangeWritingGroupCorrectionStatusRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
