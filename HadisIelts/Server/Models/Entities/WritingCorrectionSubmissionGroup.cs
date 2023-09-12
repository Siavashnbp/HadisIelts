using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingCorrectionSubmissionGroup : IEntity<string>
    {
        public string ID { get; set; }
        public List<WritingCorrectionFile> WritingCorrectionFiles { get; set; }
        public string UserID { get; set; }
        public uint TotalPrice { get; set; }
        public ApplicationUser User { get; set; }
        public string PaymentGroupID { get; set; }
        public DateTime SubmissionDateTime { get; set; }
    }
}
