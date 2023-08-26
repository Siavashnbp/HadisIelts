using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class SubmittedWritingCorrectionFiles : IEntity<string>
    {
        public string ID { get; set; }
        public List<WritingCorrectionFile> WritingCorrectionFiles { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public List<WritingPaymentPicture> PaymentPictures { get; set; }
    }
}
