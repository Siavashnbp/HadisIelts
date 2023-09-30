using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class RemoveWritingCorrectionPriceHandler : BaseMediatorRequestHandler
        <RemoveWritingCorrectionPriceRequest, RemoveWritingCorrectionPriceRequest.Response>
    {
        public RemoveWritingCorrectionPriceHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(RemoveWritingCorrectionPriceRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
