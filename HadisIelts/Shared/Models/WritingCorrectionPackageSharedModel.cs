namespace HadisIelts.Shared.Models
{
    public class WritingCorrectionPackageSharedModel
    {
        public string Id { get; set; }
        public List<ProcessedWritingFileSharedModel> ProcessedWritingFiles { get; set; }
        public uint TotalPrice { get; set; }
        public bool RequiresEmailResponse { get; set; }
        public bool IsCorrected { get; set; }
        public WritingCorrectionPackageSharedModel()
        {
            ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>();
        }
    }
}
