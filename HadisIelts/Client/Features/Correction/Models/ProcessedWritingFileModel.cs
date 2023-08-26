using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class ProcessedWritingFileModel
    {
        public WritingFileModel WritingFileModel { get; set; }
        public int WordCount { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string Message { get; set; }
        public ProcessedWritingFileModel()
        {
            WritingFileModel = new WritingFileModel();
        }
    }
}
