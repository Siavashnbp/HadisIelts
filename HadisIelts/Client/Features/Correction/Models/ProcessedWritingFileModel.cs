using HadisIelts.Client.Features.Teacher.Models;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class ProcessedWritingFileModel
    {
        public string Name { get; set; }
        public int WordCount { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string Message { get; set; }
        public WritingTypeModel WritingType { get; set; }
        public ProcessedWritingFileModel()
        {
            WritingType = new WritingTypeModel();
        }
    }
}
