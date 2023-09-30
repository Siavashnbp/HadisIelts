using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class AddWritingCorrectionServicePriceHandler : BaseMediatorRequestHandler
        <AddWritingCorrectionPriceRequest, AddWritingCorrectionPriceRequest.Response>
    {
        public AddWritingCorrectionServicePriceHandler() : base(AddWritingCorrectionPriceRequest.EndPointUri)
        {
        }
    }
}
