using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class PaymentGroup : IEntity<string>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<PaymentPicture> PaymentPictures { get; set; }
        public string SubmittedServiceId { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public bool IsPaymentApproved { get; set; }
        public bool IsPaymentCheckPending { get; set; }
        public string Message { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
