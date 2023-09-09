using HadisIelts.Shared.Models;

namespace HadisIelts.Client.Services.Payment
{
    public interface IPaymentServices<TService>

    {
        public PaymentGroupSharedModel<TService> GetPaymentData(string paymentID);
    }
}
