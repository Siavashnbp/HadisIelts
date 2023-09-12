using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record GetPaymentGroupRequest(string PaymentID) : IRequest<GetPaymentGroupRequest.Response>
    {
        public const string EndpointUri = "/api/getWritingCorrectionGroup/paymentID";
        public record Response(PaymentGroupSharedModel<WritingCorrectionPackageSharedModel> PaymentGroup);
    }
}
