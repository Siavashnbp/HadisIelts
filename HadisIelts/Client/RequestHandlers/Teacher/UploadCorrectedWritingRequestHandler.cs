using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class UploadCorrectedWritingRequestHandler : BaseMediatorRequestHandler
        <UploadCorrectedWritingRequest, UploadCorrectedWritingRequest.Response>
    {
        public UploadCorrectedWritingRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(UploadCorrectedWritingRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
