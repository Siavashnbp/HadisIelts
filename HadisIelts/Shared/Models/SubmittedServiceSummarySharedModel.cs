namespace HadisIelts.Shared.Models
{
    public class SubmittedServiceSummarySharedModel
    {
        public UserInformationSharedModel? UserDetails { get; set; }
        public string? PaymentId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? SubmittedServiceId { get; set; }
        public DateTime? SubmissionDateTime { get; set; }
        public bool IsCorrected { get; set; }
    }
}
