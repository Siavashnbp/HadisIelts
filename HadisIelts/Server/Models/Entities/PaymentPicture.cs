using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class PaymentPicture : IEntity<int>

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string FileSuffix { get; set; }
        public bool IsVerified { get; set; }
        public bool IsVerificationPending { get; set; }
        public string Message { get; set; }
        public DateTime UploadDateTime { get; set; }
        public string PaymentGroupId { get; set; }
        public PaymentGroup PaymentGroup { get; set; }
    }
}
