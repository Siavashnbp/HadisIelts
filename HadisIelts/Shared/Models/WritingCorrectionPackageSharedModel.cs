namespace HadisIelts.Shared.Models
{
    public class WritingCorrectionPackageSharedModel
    {
        public string ID { get; set; }
        public List<ProcessedWritingFileSharedModel> ProcessedWritingFiles { get; set; }
        public uint TotalPrice { get; set; }
        public bool RequiresEmailResponse { get; set; }
        public WritingCorrectionPackageSharedModel()
        {
            ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>();
        }
    }
}
