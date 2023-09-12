using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetUserSubmittedWritingCorrectionRequest(string UserID)
        : IRequest<GetUserSubmittedWritingCorrectionRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/GetUserSubmissions";
        public record Response(List<SubmittedServiceSummarySharedModel> SubmittedServices);
    }
}
