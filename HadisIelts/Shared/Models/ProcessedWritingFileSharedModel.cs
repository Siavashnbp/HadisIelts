namespace HadisIelts.Shared.Models
{
    public class ProcessedWritingFileSharedModel
    {
        public WritingFileSharedModel WritingFile { get; set; }
        public PriceGroupSharedModel PriceGroup { get; set; }
        public CorrectedWritingSharedModel? CorrectedWriting { get; set; }
        public string Message { get; set; }

    }
}
