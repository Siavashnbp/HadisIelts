using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class RemoveWritingCorrectionPriceHandler : BaseMediatorRequestHandler
        <RemoveWritingCorrectionPriceRequest, RemoveWritingCorrectionPriceRequest.Response>
    {
        public RemoveWritingCorrectionPriceHandler() : base(RemoveWritingCorrectionPriceRequest.EndPointUri)
        {
        }
    }
}
