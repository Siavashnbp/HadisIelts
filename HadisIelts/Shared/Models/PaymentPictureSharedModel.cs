namespace HadisIelts.Shared.Models
{
    public class PaymentPictureSharedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string FileSuffix { get; set; }
        public bool IsVerified { get; set; }
        public bool IsVerificationPending { get; set; }
        public string Message { get; set; }
        public DateTime UploadDateTime { get; set; }
    }
}
