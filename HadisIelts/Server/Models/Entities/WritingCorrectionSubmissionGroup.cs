using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingCorrectionSubmissionGroup : IEntity<string>
    {
        public string Id { get; set; }
        public List<WritingCorrectionFile> WritingCorrectionFiles { get; set; }
        public string UserId { get; set; }
        public uint TotalPrice { get; set; }
        public ApplicationUser User { get; set; }
        public string PaymentGroupId { get; set; }
        public DateTime SubmissionDateTime { get; set; }
        public bool IsCorrected { get; set; }
        public bool RequiresEmailResponse { get; set; }
        public List<CorrectedWritingFile> CorrectedWritingFiles { get; set; }
    }
}
