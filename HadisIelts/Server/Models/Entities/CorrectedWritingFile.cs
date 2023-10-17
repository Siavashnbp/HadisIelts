using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class CorrectedWritingFile : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string ContentType { get; set; }
        public int WritingCorrectionFileId { get; set; }
        public string WritingCorrectionSubmissionGroupId { get; set; }
        public WritingCorrectionSubmissionGroup WritingCorrectionSubmissionGroup { get; set; }
        public string CorrectorId { get; set; }
        public DateTime UploadDateTime { get; set; }
        public const string FileSuffix = "docx";
    }
}
