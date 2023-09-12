using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record GetAllSubmittedWritingCorrectionsSummaryRequest(string SearchPhrase)
        : IRequest<GetAllSubmittedWritingCorrectionsSummaryRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/getAllWritingCorrections";
        public record Response(List<SubmittedServiceSummarySharedModel> SubmittedServices);
    }
}
