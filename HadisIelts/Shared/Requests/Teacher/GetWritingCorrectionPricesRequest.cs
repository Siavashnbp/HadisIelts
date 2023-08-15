using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record GetWritingCorrectionPricesRequest()
        : IRequest<GetWritingCorrectionPricesRequest.Response>
    {
        public const string EndPointUri = "/api/services/getWritingCorrectionPrices";
        public record Response(List<WritingCorrectionPrice> WritingCorrectionPrices);
    }

}
