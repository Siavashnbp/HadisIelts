using MediatR;

namespace HadisIelts.Shared.Requests.User
{
    public record UploadWritingCorrectionAllowanceRequest() : IRequest<UploadWritingCorrectionAllowanceRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/checkAllowance";
        public record Response(bool IsAllowed);
    }
}
