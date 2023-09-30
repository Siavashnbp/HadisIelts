using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class DeleteCorrectedRequestHandler : BaseMediatorRequestHandler
        <DeleteCorrectedWritingRequest, DeleteCorrectedWritingRequest.Response>
    {
        public DeleteCorrectedRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(DeleteCorrectedWritingRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
