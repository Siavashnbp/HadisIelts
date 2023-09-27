namespace HadisIelts.Shared.Models
{
    public class CorrectedWritingSharedModel
    {
        public int Id { get; set; }
        public int WritingFileId { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
        public DateTime UploadDateTime { get; set; }
        public const string FileSuffix = ".docx";

    }
}
