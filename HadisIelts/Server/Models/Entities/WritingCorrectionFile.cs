using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingCorrectionFile : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public int WordCount { get; set; }
        public uint Price { get; set; }
        public string PriceName { get; set; }
        public int ApplicationWritingTypeId { get; set; }
        public ApplicationWritingType ApplicationWritingType { get; set; }
        public string WritingCorrectionSubmissionGroupId { get; set; }
        public WritingCorrectionSubmissionGroup WritingCorrectionSubmissionGroup { get; set; }
        public int? CorrectedWritingFileId { get; set; }
    }
}
