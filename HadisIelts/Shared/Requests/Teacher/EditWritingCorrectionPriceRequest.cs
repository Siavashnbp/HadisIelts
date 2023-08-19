using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record EditWritingCorrectionPriceRequest(WritingCorrectionPrice WritingCorrectionPrice)
        : IRequest<EditWritingCorrectionPriceRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/editWritingCorrectionPrice";
        public record Response(WritingCorrectionPrice UpdatedWritingCorrectionPrice);
    }
}
