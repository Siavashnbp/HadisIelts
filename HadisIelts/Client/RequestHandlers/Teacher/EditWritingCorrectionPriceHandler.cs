using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class EditWritingCorrectionPriceHandler : BaseMediatorRequestHandler
        <EditWritingCorrectionPriceRequest, EditWritingCorrectionPriceRequest.Response>
    {
        public EditWritingCorrectionPriceHandler(HttpClient httpClient)
            : base(httpClient, EditWritingCorrectionPriceRequest.EndpointUri)
        {
        }
    }
}
