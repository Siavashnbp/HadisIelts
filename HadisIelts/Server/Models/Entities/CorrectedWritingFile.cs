using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class CorrectedWritingFile : IEntity<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public int WritingCorrectionFileID { get; set; }
        public string CorrectorID { get; set; }
        public DateTime UploadDateTime { get; set; }
        public const string FileSuffix = "docx";
    }
}
