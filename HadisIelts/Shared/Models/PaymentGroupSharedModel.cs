namespace HadisIelts.Shared.Models
{
    public class PaymentGroupSharedModel<TService>
    {
        public string Id { get; set; }
        public string SubmittedServiceId { get; set; }
        public TService Service { get; set; }
        public DateTime UploadDateTime { get; set; }
        public bool IsPaymentApproved { get; set; }
        public bool IsPaymentCheckPending { get; set; }
        public string Message { get; set; }
        public List<PaymentPictureSharedModel> PaymentPictures { get; set; }

    }
}
