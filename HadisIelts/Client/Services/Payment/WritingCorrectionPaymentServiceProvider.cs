using HadisIelts.Shared.Models;
namespace HadisIelts.Client.Services.Payment
{
    public class WritingCorrectionPaymentServiceProvider : IPaymentServices<WritingCorrectionPackageSharedModel>
    {
        public PaymentGroupSharedModel<WritingCorrectionPackageSharedModel> GetPaymentData(string paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
