using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class AddWritingCorrectionServicePriceHandler : BaseMediatorRequestHandler
        <AddWritingCorrectionPriceRequest, AddWritingCorrectionPriceRequest.Response>
    {
        public AddWritingCorrectionServicePriceHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(AddWritingCorrectionPriceRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
