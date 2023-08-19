using HadisIelts.Shared.Requests.Teacher;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetWritingCorrectionPricesRequest()
        : IRequest<GetWritingCorrectionPricesRequest.Response>
    {
        public const string EndPointUri = "/api/services/getWritingCorrectionPrices";
        public record Response(List<WritingCorrectionPrice> WritingCorrectionPrices);
    }

}
